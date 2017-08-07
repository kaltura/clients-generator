package com.kaltura.client;

import java.text.SimpleDateFormat;
import java.util.Date;

/**
 * Created by tehilarozin on 24/08/2016.
 */
public class LoggerOut implements ILogger {
    @Override
    public boolean isEnabled() {
        return true;
    }

    @Override
    public void trace(Object message) {
        systemOutMsg("trace: ", message);
    }

    @Override
    public void debug(Object message) {
        systemOutMsg("debug: ", message);
    }

    @Override
    public void info(Object message) {
        systemOutMsg(">> info: ", message);
    }

    @Override
    public void warn(Object message) {
        systemOutMsg("# warn: ", message);
    }

    @Override
    public void error(Object message) {
        systemOutMsg("** error: ", message);
    }

    @Override
    public void fatal(Object message) {
        systemOutMsg("!! fatal: ", message);
    }

    @Override
    public void trace(Object message, Throwable t) {
        systemOutMsg("traceThrowable: ", message + "\n "+t);
    }

    @Override
    public void debug(Object message, Throwable t) {
        systemOutMsg("debugThrowable: ", message + "\n "+t);
    }

    @Override
    public void info(Object message, Throwable t) {
        systemOutMsg(">> infoThrowable: ", message + "\n "+t);
    }

    @Override
    public void warn(Object message, Throwable t) {
        systemOutMsg("# warnThrowable: ", message + "\n "+t);
    }

    @Override
    public void error(Object message, Throwable t) {
        systemOutMsg("** errorThrowable: ", message + "\n "+t);
    }

    @Override
    public void fatal(Object message, Throwable t) {
        systemOutMsg("!! fatalThrowable: ", message + "\n "+t);
    }


    private void systemOutMsg(String prefix, Object message) {
        System.out.println(getTime() + prefix + message+"\n");
    }

    public static ILogger getLogger(String name) {
        return new LoggerOut();
    }

    public static String getTime(){
        return new SimpleDateFormat("HH.mm.ss.SSS").format(new Date()) + ": ";
    }

}
