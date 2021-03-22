package kalturaclient

type Logger interface {
	Info(args ...interface{})
	Warn(args ...interface{})
	Fatal(args ...interface{})
	Debug(args ...interface{})
	Warning(args ...interface{})
	Error(args ...interface{})
	Tracef(format string, args ...interface{})
	Trace(args ...interface{})
	Debugf(format string, args ...interface{})
	Printf(format string, args ...interface{})
	Infof(format string, args ...interface{})
	Warnf(format string, args ...interface{})
	Warningf(format string, args ...interface{})
	Errorf(format string, args ...interface{})
	Fatalf(format string, args ...interface{})
	Logf(format string, args ...interface{})
	Log(args ...interface{})
}
