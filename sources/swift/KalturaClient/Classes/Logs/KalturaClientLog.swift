//
//  KalturaClientLogProtocol.swift
//  Pods
//
//  Created by Rivka Peleg on 27/07/2017.
//
//
import Foundation
import Log


/**
 Logger instance, use this macro to log all over the project.
 
 ## Example:
    logger.error("Object was not initialized")
    
    When the default implementor is assigned the above line will print:
    KalturaClient: [2017-07-30 10:51:37.284] ViewController.viewDidLoad():24 ERROR: Object was not initialized ❌
 */
public let logger = KalturaLogger(logger: KalturaClientLogImplementor())



/**
 Log level types
 */
public enum LogLevel {
    case trace, debug, info, warning, error
}


/**
 KalturaClientLogProtocol - a protocol which represent a logger that can be used by the "Logger" class.
 */
public protocol KalturaClientLogProtocol {
    
    var enabled: Bool {set get}
    var minLevel: LogLevel {set get}
    func log(_ level: LogLevel, message: Any, file: String, line: Int, column: Int, function: String)
}


/**
 KalturaLogger - Logger class that designed to be a warpper class to a logger implementor.
 Initialized by the KalturaClientLogProtocol which represent any implementation of logging that can be replaced in the future.
 */
public class KalturaLogger {
    private var logger: KalturaClientLogProtocol
    
    init(logger: KalturaClientLogProtocol) {
        self.logger = logger
    }
    
    public func trace(_ message: Any, file: String = #file, line: Int = #line, column: Int = #column, function: String = #function){
        logger.log(.trace, message: message, file: file, line: line, column: column, function: function)
    }
    
    public func debug(_ message: Any, file: String = #file, line: Int = #line, column: Int = #column, function: String = #function){
        logger.log(.debug, message: message, file: file, line: line, column: column, function: function)
    }
    
    public func info(_ message: Any, file: String = #file, line: Int = #line, column: Int = #column, function: String = #function){
        logger.log(.info, message: message, file: file, line: line, column: column, function: function)
    }

    public func warning(_ message: Any, file: String = #file, line: Int = #line, column: Int = #column, function: String = #function){
        logger.log(.warning, message: message, file: file, line: line, column: column, function: function)
    }

    public func error(_ message: Any, file: String = #file, line: Int = #line, column: Int = #column, function: String = #function){
        logger.log(.error, message: message, file: file, line: line, column: column, function: function)
    }

}

/**
 Logger implementor - using "Log" framework
 */
private class KalturaClientLogImplementor: KalturaClientLogProtocol {
    
    private let logger = Logger(formatter: .KalturaFormatter, theme: nil)
    
    public var enabled: Bool {
        get {
            return logger.enabled
        }
        set(value) {
            logger.enabled = value
        }
    }
    
    public var minLevel: LogLevel{
        get{
            return toLogLevel(level: logger.minLevel)
        }
        set (value){
            logger.minLevel = toLevel(level: value)
        }
    }
    
    func log(_ level: LogLevel, message: Any, file: String , line: Int , column: Int , function: String ){
        
        switch level {
        case .trace:
            logger.trace(message, terminator:" ✉️ \n", file: file, line: line, column: column, function: function)
        case .debug:
            logger.debug(message, terminator:" 🛠 \n", file: file, line: line, column: column, function: function)
        case .info:
            logger.info(message, terminator:" 💡 \n", file: file, line: line, column: column, function: function)
        case .warning:
            logger.warning(message, terminator:" 👆 \n", file: file, line: line, column: column, function: function)
        case .error:
            logger.error(message, terminator:" ❌ \n", file: file, line: line, column: column, function: function)
        }
    }
    
   
    
    func toLevel(level:LogLevel) -> Level {
        switch level {
        case .trace: return .trace
        case .debug: return .debug
        case .info: return .info
        case .warning: return .warning
        case .error: return .error
        }
    }
    
    func toLogLevel(level: Level) -> LogLevel {
        switch level {
        case .trace: return .trace
        case .debug: return .debug
        case .info: return .info
        case .warning: return .warning
        case .error: return .error
        }
    }
}

extension Formatters {
    static let KalturaFormatter = Formatter("KalturaClient: [%@] %@.%@:%@ %@: %@", [
        .date("yyyy-MM-dd HH:mm:ss.SSS"),
        .file(fullPath: false, fileExtension: false),
        .function,
        .line,
        .level,
        .message
        ])
}





