FROM yugabytedb/yugabyte:2.21.1.0-b271
COPY docker-healthcheck.sh /usr/local/bin/docker-healthcheck.sh
RUN chmod +x /usr/local/bin/docker-healthcheck.sh
HEALTHCHECK --interval=10s CMD ["bash", "docker-healthcheck.sh"]
