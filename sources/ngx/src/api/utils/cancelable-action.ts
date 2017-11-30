export type CancelFn = () => void;
export type ResolveFn = (value?: any) => any | void;
export type RejectFn  = (reason?: Error) => any | void;

export  class CancelableAction
{
    private _executors  : {resolve: ResolveFn, reject: RejectFn}[] = [];
    private _onCancel : CancelFn | void;
    constructor(executor : (resolve : ResolveFn, reject : RejectFn) => CancelFn | void)
    {
        this._onCancel = executor(this._onResolve.bind(this), this._onReject.bind(this));
    }

    private _onResolve(value? : any ) : void
    {
        this._notifyExecutor(value,true);
    }

    private _onReject(reason? : any ) : void
    {
        this._notifyExecutor(reason,false);
    }

    private _notifyExecutor(value : any, isResolved : boolean) : void{
        const nextExecutor = this._executors.length > 0 ? this._executors.splice(0,1)[0] : null;
        let newValue : any;
        if (nextExecutor)
        {
            try {
                if (isResolved) {
                    newValue = nextExecutor.resolve(value);
                } else {
                    if (nextExecutor.reject) {
                        newValue = nextExecutor.reject(value);
                    }else {
                        this._handleErrorOriginatedFromExecuter(value);

                    }
                }

                newValue = typeof newValue === 'undefined' ? value : newValue;
                this._notifyExecutor(newValue,true);
            }catch(e)
            {
                this._handleErrorOriginatedFromExecuter(e);
            }
        }
    }

    private _handleErrorOriginatedFromExecuter(reason : any) : void {
        if (this._executors.length > 0)
        {
            this._notifyExecutor(reason,false);
        }else {
            throw reason;
        }
    }

    cancel() : void {
        if (this._onCancel) {
            this._onCancel();
        }
    }

    then(resolve: ResolveFn, reject: RejectFn) : this
    {
        this._executors.push({resolve, reject});
        return this;
    }
}