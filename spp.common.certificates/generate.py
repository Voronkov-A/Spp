#!/usr/bin/env python3

import argparse, getpass, os, subprocess, traceback
from typing import List


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument(
        '--password',
        help='Password. It will be requested safely, if not specified.',
        required=False
    )
    parser.add_argument(
        '--common-name',
        help='Common name',
        required=True
    )
    parser.add_argument(
        '--san',
        help='Subject alternative name.',
        nargs='*'
    )
    args = parser.parse_args()
    
    password: str = args.password or getpass.getpass('Password: ')
    common_name: str = args.common_name
    san: List[str] = args.san or []
    
    generate_certificate(
        f'certificates/{common_name}',
        common_name,
        password,
        'spp-ca/spp-ca.pfx',
        extensions=[f'subjectAltName = DNS:{item}' for item in san]
    )

def generate_certificate(
    dst_dir: str,
    common_name: str,
    password: str,
    ca: str = None,
    ca_key: str = None,
    extensions: List[str] = []
):
    def execute_openssl(args: List[str]) -> None:
        process = subprocess.run(['openssl', *args])
        if process.returncode != 0:
            print(process.stderr)
        process.check_returncode()
    
    if not os.path.isdir(dst_dir):
        os.makedirs(dst_dir)
    ca_args = [] if ca is None else ['-CA', ca]
    ca_key_args = [] if ca_key is None else ['-CAkey', ca_key]
    extension_args = [
        item
        for items in [['-addext', extension] for extension in extensions]
        for item in items
    ]
    execute_openssl([
        'req',
        '-new',
        '-newkey', 'rsa:2048',
        '-days', '365250',
        '-nodes',
        '-x509',
        '-keyout', f'{dst_dir}/{common_name}.key',
        '-out', f'{dst_dir}/{common_name}.crt',
        *ca_args,
        *ca_key_args,
        *extension_args,
        '-subj', f'/C=HN/ST=Galactic Empire/L=Coruscant/O=Vader Security/OU=Death Star Department/CN={common_name}'
    ])
    execute_openssl([
        'pkcs12',
        '-export',
        '-inkey', f'{dst_dir}/{common_name}.key',
        '-in', f'{dst_dir}/{common_name}.crt',
        '-out', f'{dst_dir}/{common_name}.pfx',
        '-passout', f'pass:{password}'
    ])
    execute_openssl([
        'pkcs12',
        '-in', f'{dst_dir}/{common_name}.pfx',
        '-out', f'{dst_dir}/{common_name}.pem',
        '-passin', f'pass:{password}',
        '-nodes'
    ])

if __name__ == "__main__":
    try:
        main()
    except:
        with open(f'{__file__}.error.txt', 'w') as error_file:
            error_file.write(traceback.format_exc())
        raise
