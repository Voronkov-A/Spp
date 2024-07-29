#!/usr/bin/env python3

import argparse, getpass, importlib, importlib.util, os, shutil, subprocess, sys, traceback
from pathlib import Path
from typing import List, Set, Union
from unittest.mock import patch


def main():
    target_paths = [
        'spp.services.identity-provider/certificates/localhost.crt',
        'spp.services.identity-provider/certificates/localhost.key',
        'spp.services.authorization/certificates/localhost.crt',
        'spp.services.authorization/certificates/localhost.key'
    ]

    parser = argparse.ArgumentParser()
    parser.add_argument(
        '--rebuild',
        help='Rebuild certificates before copying.',
        required=False,
        dest='rebuild',
        action='store_true')
    parser.set_defaults(rebuild=False)
    args = parser.parse_args()

    rebuild: bool = args.rebuild

    if rebuild:
        execute(Path('../spp.common.certificates'), [ 'build.py', ]);

    for target_path in target_paths:
        dst = Path(f'../{target_path}')
        file_name = dst.name

        if file_name.startswith('localhost.'):
            src = Path('../spp.common.certificates/localhost') / file_name
        elif file_name.startswith('spp-ca'):
            src = Path('../spp.common.certificates/spp-ca') / file_name
        else:
            raise RuntimeError(f"Invalid target path: '{target_path}'.")
        
        if not dst.parent.exists():
            os.makedirs(dst.parent)
        elif dst.exists():
            os.remove(dst)
        shutil.copy(src, dst)

def execute(pwd: str, args: List[str]) -> None:
    cwd = os.getcwd()
    try:
        with patch('sys.argv', args):
            os.chdir(pwd)
            module_name = args[0]
            file_name = Path(args[0])
            spec = importlib.util.spec_from_file_location(module_name, file_name)
            module = importlib.util.module_from_spec(spec)
            spec.loader.exec_module(module)
            func = getattr(module, 'main')
            func()
    finally:
        os.chdir(cwd)

if __name__ == "__main__":
    try:
        main()
    except:
        with open(f'{__file__}.error.txt', 'w') as error_file:
            error_file.write(traceback.format_exc())
        raise
