#!/usr/bin/env python3

import argparse, importlib, importlib.util, os, sys, traceback
from typing import List, Set
from pathlib import Path
from unittest.mock import patch


def main() -> None:
    all_modules = [
        'spp.common.miscellaneous',
        'spp.common.domain',
        'spp.common.configuration',
        'spp.common.exceptions',
        'spp.common.errors',
        'spp.common.transactions',
        'spp.common.mediator',
        'spp.common.initialization',
        'spp.common.subscriptions',
        'spp.common.postgres',
        'spp.common.migrations',
        'spp.common.eventsourcing',
        'spp.common.http',
        'spp.common.authentication',
        'spp.common.authorization',
        'spp.common.localization',
        'spp.common.logging',
        'spp.common.cqs',
        'spp.common.hosting',
        'spp.common.test-helpers',
        'spp.common.openapi-generator',

        'spp.services.yugabyte.master',
        'spp.services.yugabyte.tserver',
        'spp.services.identity-provider',
        'spp.services.authorization'
    ]

    parser = argparse.ArgumentParser()
    parser.add_argument(
        'modules',
        help='Modules to build.',
        nargs='*'
    )
    args = parser.parse_args()
    modules_to_build: Set[str] = set(args.modules)

    if len(modules_to_build) == 0:
        modules_to_build.add('*')
    
    for module in all_modules:
        path = Path(f'../{module}')
        print(f'Building and installing {path}...')
        if (path / 'build.py').exists():
            execute(path, ['build.py'])
        if (path / 'install.py').exists():
            execute(path, ['install.py'])
        
        modules_to_build.discard(module)
        if len(modules_to_build) == 0:
            break

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
