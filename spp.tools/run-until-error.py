#!/usr/bin/env python3

import datetime, os, subprocess, sys

def main():
    
    if len(sys.argv) < 3:
        print('Expected arguments. Example: run-until-error.py working_directory command')

    start_time = datetime.datetime.now()
    counter = 0
    pwd = sys.argv[1]
    cwd = os.getcwd()
    try:
        os.chdir(pwd)
        while True:
            subprocess.run(sys.argv[2:]).check_returncode()
            counter = counter + 1
    finally:
        os.chdir(cwd)
        end_time = datetime.datetime.now()
        print(f'From {start_time} to {end_time}. {counter} successful executions.')

if __name__ == "__main__":
    main()
