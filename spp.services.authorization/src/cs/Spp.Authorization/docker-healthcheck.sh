#!/bin/sh

if [ "$(curl https://localhost:$1/v1/service/health -sk)" = "\"healthy\"" ]; then
    exit 0
else
    exit 1
fi
