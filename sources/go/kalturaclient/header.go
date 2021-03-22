package kalturaclient

const (
	RequestIdHeader = "x-kaltura-session-id"
)

type Header struct {
	key                        string
	value                      string
	wasCreatedUsingConstructor bool
}

func RequestId(value string) Header {
	return Header{
		key:                        RequestIdHeader,
		value:                      value,
		wasCreatedUsingConstructor: true,
	}
}
