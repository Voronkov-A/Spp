#!/usr/bin/env python3

import argparse, subprocess, traceback

def main():
    parser = argparse.ArgumentParser()
    parser.add_argument(
        '--registry',
        help='Registry.',
        required=False,
        default='localhost:51443'
    )
    args = parser.parse_args()

    registry: str = args.registry
    version = '0.0.1'
    
    subprocess.run([
        'docker', 'build', 'src',
        '-t', f'{registry}/spp/yugabyte-tserver:{version}'
    ]).check_returncode()

if __name__ == "__main__":
    try:
        main()
    except:
        with open(f'{__file__}.error.txt', 'w') as error_file:
            error_file.write(traceback.format_exc())
        raise
