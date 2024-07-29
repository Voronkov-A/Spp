#!/usr/bin/env python3

import argparse, subprocess, traceback
from pathlib import Path


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument(
        '--docker-config-json-path',
        help='Docker config.json path. By default $HOME/.docker/config.json will be used.',
        required=False
    )
    parser.add_argument(
        '--identity-provider-tls-crt-path',
        help='Path to public tls.crt file for identity-provider. By default certificates/identity-provider/identity-provider.minikube.crt will be used.',
        required=False
    )
    parser.add_argument(
        '--identity-provider-tls-key-path',
        help='Path to public tls.key file for identity-provider. By default certificates/identity-provider/identity-provider.minikube.key will be used.',
        required=False
    )
    parser.add_argument(
        '--identity-provider-ca-crt-path',
        help='Path to public ca.crt file for identity-provider. By default certificates/identity-provider/spp-ca.crt will be used.',
        required=False
    )
    args = parser.parse_args()
    
    docker_config_json_path: str = args.docker_config_json_path or str(Path.home() / '.docker/config.json').replace('\\', '/')
    identity_provider_tls_crt_path: str = args.identity_provider_tls_crt_path or 'certificates/identity-provider/identity-provider.minikube.crt'
    identity_provider_tls_key_path: str = args.identity_provider_tls_key_path or 'certificates/identity-provider/identity-provider.minikube.key'
    identity_provider_ca_crt_path: str = args.identity_provider_ca_crt_path or 'certificates/identity-provider/spp-ca.crt'

    # TODO: install only when it is not already installed
    #subprocess.run([
    #    'helm', 'plugin', 'install', 'https://github.com/databus23/helm-diff'
    #]).check_returncode()

    dockerconfigjson = f'ref+file://{docker_config_json_path}'

    subprocess.run([
        'helmfile', 'apply',
        '--state-values-file', 'state-values.yaml',
        '--state-values-set', f'identityProvider.common.imagePullSecret.dockerconfigjson={dockerconfigjson}',
        '--state-values-set', f'identityProvider.gateway.config.tls.crt=ref+file://{identity_provider_tls_crt_path}',
        '--state-values-set', f'identityProvider.gateway.config.tls.key=ref+file://{identity_provider_tls_key_path}',
        '--state-values-set', f'identityProvider.gateway.config.ca.crt=ref+file://{identity_provider_ca_crt_path}'
    ]).check_returncode()

if __name__ == "__main__":
    try:
        main()
    except:
        with open(f'{__file__}.error.txt', 'w') as error_file:
            error_file.write(traceback.format_exc())
        raise
