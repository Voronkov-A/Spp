#!/usr/bin/env python3

from pathlib import Path
import subprocess
import traceback

def main():
    source_url = Path.cwd() / 'nuget'
    source_name = Path.cwd()

    subprocess.run(['dotnet', 'nuget', 'remove', 'source', source_name], stdout=subprocess.DEVNULL)
    subprocess.run(['dotnet', 'nuget', 'add', 'source', source_url, '--name', source_name])\
        .check_returncode()

if __name__ == "__main__":
    try:
        main()
    except:
        with open(f'{__file__}.error.txt', 'w') as error_file:
            error_file.write(traceback.format_exc())
        raise
