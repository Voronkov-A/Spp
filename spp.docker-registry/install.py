#!/usr/bin/env python3

import argparse, getpass, os, shutil, subprocess
import traceback

def main():
    parser = argparse.ArgumentParser()
    parser.add_argument(
        '--username',
        '-u',
        help='Docker registry user login. It will be requested, if not specified.',
        required=False
    )
    parser.add_argument(
        '--password',
        help='Docker registry user password. It will be requested safely, if not specified.',
        required=False
    )
    parser.add_argument(
        '--crt-path',
        help='Certificate (.crt) path.',
        required=True
    )
    parser.add_argument(
        '--key-path',
        help='Key (.key) path.',
        required=True
    )
    args = parser.parse_args()

    username: str = args.username or input('Docker registry user login: ')
    password: str = args.password or getpass.getpass(f'Docker registry user ({username}) password: ')
    crt_path: str = args.crt_path
    key_path: str = args.key_path


    os.makedirs('certificates', exist_ok=True)
    shutil.copy(crt_path, 'certificates/localhost.crt')
    shutil.copy(key_path, 'certificates/localhost.key')


    os.makedirs('auth', exist_ok=True)
    with open('auth/htpasswd', 'w') as file:
        process = subprocess.run(
            [
                'docker', 'run',
                '--entrypoint', 'htpasswd',
                '--rm',
                'httpd:2', '-Bbn', username, password
            ],
            stdout=file
        )
        process.check_returncode()

if __name__ == "__main__":
    try:
        main()
    except:
        with open(f'{__file__}.error.txt', 'w') as error_file:
            error_file.write(traceback.format_exc())
        raise
