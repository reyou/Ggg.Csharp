g.withStrategies(
  PartitionStrategy.build()
    .partitionKey("partitionKey")
    .readPartitions("partitionKey_value")
    .create()
).V();
