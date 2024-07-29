#!/usr/bin/env python3

import argparse, getpass, os, subprocess, traceback
from typing import List, Union


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument(
        '--password',
        help='Password. It will be requested safely, if not specified.',
        required=False
    )
    args = parser.parse_args()

    password: str = args.password or getpass.getpass('Password: ')

    generate_certificate('spp-ca', 'spp-ca', password)

def generate_certificate(
    dst_dir: str,
    common_name: str,
    password: str,
    ca: str = None,
    ca_key: str = None,
    extension: Union[str, None] = None
):
    def execute_openssl(args: List[str]) -> None:
        process = subprocess.run(['openssl', *args])
        if process.returncode != 0:
            print(process.stderr)
        process.check_returncode()

    if not os.path.isdir(dst_dir):
        os.mkdir(dst_dir)
    ca_args = [] if ca is None else ['-CA', ca]
    ca_key_args = [] if ca_key is None else ['-CAkey', ca_key]
    extension_args = [] if extension is None else ['-addext', extension]
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
