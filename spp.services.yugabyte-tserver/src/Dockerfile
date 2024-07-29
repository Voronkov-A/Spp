FROM yugabytedb/yugabyte:2.21.1.0-b271
COPY docker-healthcheck.sh /usr/local/bin/docker-healthcheck.sh
RUN chmod +x /usr/local/bin/docker-healthcheck.sh
HEALTHCHECK --interval=10s CMD ["bash", "docker-healthcheck.sh"]
EXPOSE 5433
EXPOSE 9100
CMD [ "/bin/sh", "-c", "/home/yugabyte/bin/yb-tserver --fs_data_dirs=/mnt/tserver --start_pgsql_proxy --rpc_bind_addresses=$RPC_BIND_ADDRESSES --tserver_master_addrs=$TSERVER_MASTER_ADDRS --ysql_enable_auth" ]
