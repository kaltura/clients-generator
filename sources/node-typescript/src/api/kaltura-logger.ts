

const Levels = ['verbose', 'debug', 'info', 'warn', 'error', 'fatal'] as const
type TLevel = typeof Levels[number]

const hostLogger = {
  logger: null
}

const ClientLogger = {
  get(target, prop) {
    if(!Levels.includes(prop)) {
      throw new Error('Invalid usage of Kaltura Logger')
    }
    return target.logger?.[prop] || (() => {})
  }
}

export const Logger = new Proxy(hostLogger, ClientLogger);
export const initializeLogger = (logger: Partial<Record<TLevel, (msg:string) => any>>): void => { hostLogger.logger = logger}