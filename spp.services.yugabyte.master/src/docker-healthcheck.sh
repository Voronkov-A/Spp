#!/bin/sh

status=$(curl --write-out '%{http_code}' --silent --output /dev/null http://$SERVICE_7000_NAME:7000/api/v1/health-check)

if (( $status == 200 )); then
    echo 0
else
    echo 1
fi
