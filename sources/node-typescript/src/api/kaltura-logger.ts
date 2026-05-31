
const Levels = ['verbose', 'debug', 'info', 'warn', 'error', 'fatal'] as const
type TLevel = typeof Levels[number]

export const DEFAULT_SECRET_FIELDS = [
  '^ks$',
  'secret',
  'password',
  'token',
  'passphrase',
  'privatekey',
  'publickey',
  'apikey',
  'encryptionkey',
  'signingkey',
  'certificatekey',
  'sslkey',
  'streamkey',
  'salt',
  '^iv$',
  'accesskey',
  'seed',
];

const hostLogger = {
  logger: null,
  secretFields: DEFAULT_SECRET_FIELDS,
  secretPattern: null as RegExp | null
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
export const isLevelEnabled = (level: TLevel): boolean => !!hostLogger.logger?.[level];
export const initializeLogger = (logger: Partial<Record<TLevel, (msg:string) => any>>, extraSecretFields?: string[]): void => {
  hostLogger.logger = logger;
  if (extraSecretFields?.length) {
    hostLogger.secretFields = [...DEFAULT_SECRET_FIELDS, ...extraSecretFields];
  }
  hostLogger.secretPattern = new RegExp(hostLogger.secretFields.join('|'), 'i');
}

export function maskSecrets<T>(object: T): T {
  if (Array.isArray(object)) {
    return object.map(item => maskSecrets(item)) as T;
  }

  if (object && typeof object === "object") {
    const result = {} as T;
    for (const [key, value] of Object.entries(object)) {
      result[key] = hostLogger.secretPattern?.test(key) ? `[masked:${String(value).length}]` : maskSecrets(value);
    }
    return result;
  }

  return object;
}
