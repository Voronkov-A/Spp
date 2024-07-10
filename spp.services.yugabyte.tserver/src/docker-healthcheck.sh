#!/bin/sh

export PGPASSWORD=yugabyte && bin/ysqlsh --host $SERVICE_9000_NAME --port 5433 --username yugabyte --no-password -c 'select version();'
exit $?
