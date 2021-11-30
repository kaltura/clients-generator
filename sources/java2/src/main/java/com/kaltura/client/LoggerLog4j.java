// ===================================================================================================
//                           _  __     _ _
//                          | |/ /__ _| | |_ _  _ _ _ __ _
//                          | ' </ _` | |  _| || | '_/ _` |
//                          |_|\_\__,_|_|\__|\_,_|_| \__,_|
//
// This file is part of the Kaltura Collaborative Media Suite which allows users
// to do with audio, video, and animation what Wiki platfroms allow them to do with
// text.
//
// Copyright (C) 2006-2011  Kaltura Inc.
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
// @ignore
// ===================================================================================================
package com.kaltura.client;

import org.apache.logging.log4j.LogManager;

public class LoggerLog4j implements ILogger
{
	private org.apache.logging.log4j.Logger logger;
		
	public LoggerLog4j(String name)
	{
		logger = LogManager.getLogger(name);
	}

	@Override
	public boolean isEnabled() {
		return logger.isInfoEnabled();
	}

	@Override
	public void trace(Object message) {
		logger.trace(message);
	}

	@Override
	public void debug(Object message) {
		logger.debug(message);		
	}

	@Override
	public void info(Object message) {
		logger.info(message);
	}

	@Override
	public void warn(Object message) {
		logger.warn(message);
	}

	@Override
	public void error(Object message) {
		logger.error(message);
	}

	@Override
	public void fatal(Object message) {
		logger.fatal(message);
	}

	@Override
	public void trace(Object message, Throwable t) {
		logger.trace(message, t);
	}

	@Override
	public void debug(Object message, Throwable t) {
		logger.debug(message, t);
	}

	@Override
	public void info(Object message, Throwable t) {
		logger.info(message, t);
	}

	@Override
	public void warn(Object message, Throwable t) {
		logger.warn(message, t);
	}

	@Override
	public void error(Object message, Throwable t) {
		logger.error(message, t);
	}

	@Override
	public void fatal(Object message, Throwable t) {
		logger.fatal(message, t);
	}
}
