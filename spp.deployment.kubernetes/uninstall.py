#!/usr/bin/env python3

import argparse, subprocess, traceback, yaml


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument(
        '-v',
        help='Delete PVC.',
        required=False,
        dest='volumes',
        action='store_true')
    parser.set_defaults(volumes=False)
    args = parser.parse_args()

    volumes: bool = args.volumes

    with open('state-values.yaml', 'r') as file:
        state_values = yaml.load(file, Loader=yaml.Loader)
    
    identity_provider_namespace = state_values['identityProvider']['namespace']
    subprocess.run([
        'helm', '-n', identity_provider_namespace,
        'delete', '--ignore-not-found', 'spp-identity-provider'
    ])

    if state_values['reloader']['installationRequired']:
        reloaderNamespace = state_values['reloader']['namespace']
        subprocess.run([
            'helm', '-n', reloaderNamespace,
            'delete', '--ignore-not-found', 'spp-reloader'
        ]).check_returncode()

    if state_values['certManager']['installationRequired']:
        cert_manager_namespace = state_values['certManager']['namespace']
        subprocess.run([
            'helm', '-n', cert_manager_namespace,
            'delete', '--ignore-not-found', 'spp-cert-manager'
        ]).check_returncode()
        subprocess.run([
            'kubectl', '-n', cert_manager_namespace,
            'delete', 'secret', '--ignore-not-found', f'{cert_manager_namespace}-webhook-ca'
        ]).check_returncode()

    identity_provider_secret_names = [
        'identity-provider-tls',
        'identity-provider-token-signature'
    ]

    for secret_name in identity_provider_secret_names:
        subprocess.run([
            'kubectl', '-n', identity_provider_namespace,
            'delete', 'secret', '--ignore-not-found', secret_name
        ]).check_returncode()
    
    identity_provider_volume_prefixes = [
        'data-yugabyte-master-',
        'data-yugabyte-tserver-'
    ]

    if volumes:
        get_pvc = subprocess.run([
            'kubectl',
            '-n', identity_provider_namespace,
            'get', 'pvc',
            '--no-headers',
            '-o', 'custom-columns=:metadata.name,:spec.volumeName,:status.phase'
        ], stdout=subprocess.PIPE, text=True)
        get_pvc.check_returncode()
        lines = get_pvc.stdout.splitlines()
    
        for line in lines:
            tokens = list(filter(lambda x: x != '', line.split(' ')))
            pvc_name = tokens[0]
            pv_name = tokens[1]
            pvc_status = tokens[2]
            
            if any(pvc_name.startswith(x) for x in identity_provider_volume_prefixes):
                if pvc_status == 'Bound':
                    print(f'Deleting pvc {pvc_name} bound to {pv_name}...')
                    subprocess.run([
                        'kubectl', '-n', identity_provider_namespace, 'delete', 'pvc', pvc_name
                    ]).check_returncode()
                else:
                    print(f'Pvc {pvc_name} is in status {pvc_status}. Leave it as is.')


if __name__ == "__main__":
    try:
        main()
    except:
        with open(f'{__file__}.error.txt', 'w') as error_file:
            error_file.write(traceback.format_exc())
        raise
