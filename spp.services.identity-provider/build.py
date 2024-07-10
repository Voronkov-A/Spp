#!/usr/bin/env python3

import argparse, shutil, subprocess, traceback
from pathlib import Path

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


    dir = Path('build')
    if dir.exists():
        shutil.rmtree(dir)
    dir.mkdir()
    
    subprocess.run([
        'dotnet', 'pack', 'src/cs',
        '-c', 'Release',
        '-o', 'build'
    ]).check_returncode()


    project_dir = 'src/cs/Spp.IdentityProvider'
    image_name = 'identity-provider'
    version = '0.0.1'

    bin_dir = Path(f'{project_dir}/bin')
    if bin_dir.exists():
        shutil.rmtree(bin_dir)
    
    subprocess.run([
        'dotnet',
        'publish', project_dir,
        '-c', 'Release'
    ]).check_returncode()

    subprocess.run([
        'docker',
        'build', project_dir,
        '-t', f'{registry}/spp/{image_name}:{version}'
    ]).check_returncode()

if __name__ == "__main__":
    try:
        main()
    except:
        with open(f'{__file__}.error.txt', 'w') as error_file:
            error_file.write(traceback.format_exc())
        raise
