export interface Environment {
    request: {
        apiVersion: string,
        ottMode: boolean,
        fileFormatValue: number
    }
    response: {
        nestedResponse: boolean,
        customErrorInHttp500: boolean
    };
}

export const environment: Environment = {
    request: {
        apiVersion: '18.0.0',
        ottMode: false,
        fileFormatValue: 1
    },
    response: {
        nestedResponse: false,
        customErrorInHttp500: false
    }
}
