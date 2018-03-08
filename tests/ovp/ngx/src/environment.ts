export interface Environment {
    response: {
        nestedResponse: boolean
    };
}

export const environment: Environment = {
    response: {
        nestedResponse: false
    }
}