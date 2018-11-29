export type CancelFn = () => void;
export type ResolveFn<T> = (value: T | null) => any | void;
export type RejectFn  = (reason: Error | {error: Error}) => any | void;

export  class CancelableAction<T>
{
    static resolve<T>(result: T): CancelableAction<T> {
        return new CancelableAction<T>((resolve) => {
            resolve(result);
        });
    }

    static reject<T>(error: Error): CancelableAction<T> {
        return new CancelableAction<T>((resolve, reject) => {
            reject(error);
        });
    }

    private _executors : {resolve: ResolveFn<T>, reject: RejectFn}[] = [];
    private _onCancel : CancelFn | void;
    constructor(executor : (resolve : ResolveFn<T>, reject : RejectFn, action?: CancelableAction<T>) => CancelFn | void)
    {
        this._onCancel = executor(this._onResolve.bind(this), this._onReject.bind(this), this);
    }

    private _onResolve(value? : T ) : void
    {
        this._notifyExecutor(value);
    }

    private _onReject(reason? : Error ) : void
    {
        this._notifyExecutor(reason);
    }

    private _notifyExecutor(value : T | Error) : void{
        const nextExecutor = this._executors.length > 0 ? this._executors.splice(0,1)[0] : null;
        let newValue : any;
        if (nextExecutor)
        {
            try {
                if (value instanceof Error) {
                    if (nextExecutor.reject) {
                        newValue = nextExecutor.reject(value);
                    }else {
                        this._handleErrorOriginatedFromExecuter(value);
                    }
                } else {
                    if (nextExecutor.resolve) {
                        newValue = nextExecutor.resolve(<T>value);
                    }
                }


                if (newValue instanceof CancelableAction)
                {
                    newValue.then(this._onResolve.bind(this), this._onReject.bind(this));
                    return;
                }else if (newValue instanceof Promise)
                {
                    newValue.then(this._onResolve.bind(this), this._onReject.bind(this));
                    return;
                }else {
                    newValue = typeof newValue === 'undefined' ? value : newValue;
                    this._notifyExecutor(newValue);
                }
            }catch(e)
            {
                this._handleErrorOriginatedFromExecuter(e);
            }
        }
    }

    private _handleErrorOriginatedFromExecuter(reason : any) : void {
        if (this._executors.length > 0)
        {
            this._notifyExecutor(reason);
        }else {
            throw reason;
        }
    }

    cancel() : void {
        if (this._onCancel) {
            this._onCancel();
        }
    }

    then(resolve: ResolveFn<T>, reject: RejectFn): this
    {
        this._executors.push({resolve, reject});
        return this;
    }

    catch(reject: RejectFn) : this
    {
        this._executors.push({resolve: null, reject});
        return this;
    }
}
