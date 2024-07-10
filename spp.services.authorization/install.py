#!/usr/bin/env python3

import os, shutil, traceback
from pathlib import Path

def main():
    files_to_copy = Path('build').glob('*.nupkg')
    
    for src in files_to_copy:
        dst = Path.joinpath(Path('../spp.repositories/nuget'), src.name)
        if dst.exists():
            os.remove(dst)
        shutil.copy(src, dst)  
    
    home = Path.home()
    
    directories_to_remove = Path.joinpath(home, '.nuget', 'packages')\
        .glob(Path.cwd().name.replace('spp.services.', 'spp.').replace('-', '') + '*')

    for dir in directories_to_remove:
        if dir.exists():
            shutil.rmtree(dir)

if __name__ == "__main__":
    try:
        main()
    except:
        with open(f'{__file__}.error.txt', 'w') as error_file:
            error_file.write(traceback.format_exc())
        raise
