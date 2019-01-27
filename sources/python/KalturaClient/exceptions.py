
# =============================================================================
#                           _  __     _ _
#                          | |/ /__ _| | |_ _  _ _ _ __ _
#                          | ' </ _` | |  _| || | '_/ _` |
#                          |_|\_\__,_|_|\__|\_,_|_| \__,_|
#
# This file is part of the Kaltura Collaborative Media Suite which allows users
# to do with audio, video, and animation what Wiki platfroms allow them to do
# with text.
#
# Copyright (C) 2006-2019  Kaltura Inc.
#
# This program is free software: you can redistribute it and/or modify
# it under the terms of the GNU Affero General Public License as
# published by the Free Software Foundation, either version 3 of the
# License, or (at your option) any later version.
#
# This program is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
# GNU Affero General Public License for more details.
#
# You should have received a copy of the GNU Affero General Public License
# along with this program.  If not, see <http:#www.gnu.org/licenses/>.
#
# @ignore
# =============================================================================


class KalturaException(Exception):

    """Exception class for server errors."""

    def __init__(self, message, code):
        self.code = code
        self.message = message

    def __str__(self):
        return "%s (%s)" % (self.message, self.code)


class KalturaClientException(Exception):

    """Exception class for client errors."""

    ERROR_GENERIC = -1
    ERROR_INVALID_XML = -2
    ERROR_FORMAT_NOT_SUPPORTED = -3
    ERROR_CONNECTION_FAILED = -4
    ERROR_READ_FAILED = -5
    ERROR_INVALID_PARTNER_ID = -6
    ERROR_INVALID_OBJECT_TYPE = -7
    ERROR_RESULT_NOT_FOUND = -8
    ERROR_READ_TIMEOUT = -9
    ERROR_READ_GZIP_FAILED = -10

    def __init__(self, message, code):
        self.code = code
        self.message = message

    def __str__(self):
        return "%s (%s)" % (self.message, self.code)
