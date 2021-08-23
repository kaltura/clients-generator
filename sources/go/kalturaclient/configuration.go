package kalturaclient

type Configuration struct {
	ServiceUrl                string `json:"serviceUrl"  validate:"required" binding:"required"  bson:"ServiceUrl"`
	IdleConnectionTimeoutMs   int    `json:"idleConnectionTimeoutMs"  bson:"IdleConnectionTimeoutMs"`
	MaxConnectionsPerHost     int    `json:"maxConnectionsPerHost"  bson:"MaxConnectionsPerHost"`
	MaxIdleConnections        int    `json:"maxIdleConnections"  bson:"MaxIdleConnections"`
	MaxIdleConnectionsPerHost int    `json:"maxIdleConnectionsPerHost"  bson:"MaxIdleConnectionsPerHost"`
	TimeoutMs                 int    `json:"timeoutMs"  bson:"TimeoutMs"`
	UseHttps                  bool   `json:"useHttps"  bson:"UseHttps"`
}
