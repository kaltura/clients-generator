package test

/*
import (
	"fmt"
	//restServer "github.com/kaltura/ott-service-partner-accounts-setup/generated/api/rest/server"
	"net"
	"os"
	"sync/atomic"
	"time"
)

func startRest() (restPort int32) {
	restServerPort := getNewPort()
	_ = os.Setenv("OTT_LISTEN_PORT_REST", fmt.Sprint(restServerPort))
	//restServer.RunAsync()
	timeout := 5 * time.Second
	err := waitForRestConnection(localTcpUrl(restServerPort), timeout)
	if err != nil {
		panic(fmt.Sprintf("Unable to start mock REST server. error: [%e]", err))
	}
	return restServerPort
}

var port = int32(3000)

func getNewPort() int32 {
	newPort := atomic.AddInt32(&port, 1)
	return newPort
}

func localTcpUrl(port int32) string {
	return fmt.Sprintf("localhost:%d", port)
}

func waitForRestConnection(address string, timeout time.Duration) error {
	err := retry(timeout, 100*time.Millisecond, func() error {
		_, err := net.DialTimeout("tcp", address, 100*time.Millisecond)
		return err
	})
	return err
}

func retry(timeout time.Duration, sleep time.Duration, fn func() error) error {
	start := time.Now()
	return genericRetry(func() bool {
		return time.Since(start) < timeout
	}, sleep, fn)
}

// for tests only
func genericRetry(retryCondition func() bool, sleep time.Duration, fn func() error) error {
	if err := fn(); err != nil {
		if s, ok := err.(stop); ok {
			return s.error
		}
		if retryCondition() {
			time.Sleep(sleep)
			return genericRetry(retryCondition, sleep, fn)
		}
		return err
	}
	return nil
}

type stop struct {
	error
}

func SetLogLevel(level string) {
	_ = os.Setenv("OTT_LOGGER_LEVEL", level)
}

func localHttpUrl(port int32) string {
	return fmt.Sprintf("http://localhost:%d", port)
}

func SetConfigToEnvironment() {
	_ = os.Setenv("OTT_CONF_PAYLOAD", `
{
	"phoenixApi": {
    	"address": 'localhost:5000'",
    	"timeoutMs": 30000,
    	"maxConnectionsPerHost": 1024,
    	"idleConnectionTimeoutMs": 30000,
    	"maxIdleConnections":1024,
    	"maxIdleConnectionsPerHost": 1024
	}
}
`)
}


 */