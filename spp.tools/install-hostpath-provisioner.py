#!/usr/bin/env python3

import subprocess, traceback


def main():
    subprocess.run([
        'helm', 'repo', 'add', 'rimusz', 'https://charts.rimusz.net'
    ]).check_returncode()
    subprocess.run([
        'helm', 'repo', 'update'
    ]).check_returncode()
    subprocess.run([
        'helm', 'upgrade',
        '--install', 'hostpath-provisioner',
        '--namespace', 'kube-system',
        'rimusz/hostpath-provisioner'
    ]).check_returncode()

if __name__ == "__main__":
    try:
        main()
    except:
        with open(f'{__file__}.error.txt', 'w') as error_file:
            error_file.write(traceback.format_exc())
        raise
