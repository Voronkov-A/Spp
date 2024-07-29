#!/usr/bin/env python3

import argparse, subprocess, traceback
from pathlib import Path


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument(
        '--ca-crt-path',
        help='CA crt path.',
        required=True
    )
    args = parser.parse_args()

    ca_crt_path: str = args.ca_crt_path

    dst_path = f'/etc/docker/certs.d/host.minikube.internal:51443/{Path(ca_crt_path).name}'
    subprocess.run(['minikube', 'cp', ca_crt_path, dst_path]).check_returncode()

    subprocess.run(['minikube', 'addons', 'enable', 'ingress']).check_returncode()
    subprocess.run(['minikube', 'addons', 'enable', 'ingress-dns']).check_returncode()

if __name__ == "__main__":
    try:
        main()
    except:
        with open(f'{__file__}.error.txt', 'w') as error_file:
            error_file.write(traceback.format_exc())
        raise
