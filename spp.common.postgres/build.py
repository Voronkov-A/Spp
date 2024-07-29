#!/usr/bin/env python3

import shutil, subprocess, traceback
from pathlib import Path

def main():
    dir = Path('build')
    if dir.exists():
        shutil.rmtree(dir)
    dir.mkdir()
    
    subprocess.run([
        'dotnet', 'pack', 'src/cs',
        '-c', 'Release',
        '-o', 'build'
    ]).check_returncode()

if __name__ == "__main__":
    try:
        main()
    except:
        with open(f'{__file__}.error.txt', 'w') as error_file:
            error_file.write(traceback.format_exc())
        raise
