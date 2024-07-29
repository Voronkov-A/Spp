FROM yugabytedb/yugabyte:2.21.1.0-b271
COPY docker-healthcheck.sh /usr/local/bin/docker-healthcheck.sh
RUN chmod +x /usr/local/bin/docker-healthcheck.sh
HEALTHCHECK --interval=10s CMD ["bash", "docker-healthcheck.sh"]
EXPOSE 7000
EXPOSE 7100
CMD [ "/bin/sh", "-c", "/home/yugabyte/bin/yb-master --fs_data_dirs=/mnt/master --master-addresses=$MASTER_ADDRESSES --rpc_bind_addresses=$RPC_BIND_ADDRESSES --replication_factor=$REPLICATION_FACTOR" ]
