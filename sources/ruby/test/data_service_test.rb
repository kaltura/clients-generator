# ===================================================================================================
#                           _  __     _ _
#                          | |/ /__ _| | |_ _  _ _ _ __ _
#                          | ' </ _` | |  _| || | '_/ _` |
#                          |_|\_\__,_|_|\__|\_,_|_| \__,_|
#
# This file is part of the Kaltura Collaborative Media Suite which allows users
# to do with audio, video, and animation what Wiki platfroms allow them to do with
# text.
#
# Copyright (C) 2006-2011  Kaltura Inc.
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
# ===================================================================================================
require 'test_helper'
require 'net/http'
require 'net/https'
require 'open-uri'

class DataServiceTest < Test::Unit::TestCase
  
    # this test creates a data entry and calls serve action to get a file url.
    should "get the file url with data content" do
    
      data_entry = Kaltura::KalturaDataEntry.new
      data_entry.name = "kaltura_test"
      data_entry.data_content = @content
      
      created_entry = @client.data_service.add(data_entry)
    
      assert_not_nil created_entry.id
      
      file_url = @client.data_service.serve(created_entry.id)
      
      uri = URI.parse(file_url)
      http = Net::HTTP.new(uri.host, uri.port)
      http.use_ssl = (uri.scheme == 'https')
      http.verify_mode = OpenSSL::SSL::VERIFY_NONE
      request = Net::HTTP::Get.new(uri.request_uri)
      response = http.request(request)
      assert_true (response.class == Net::HTTPFound || response.class == Net::HTTPOK)
      
      assert_nil @client.data_service.delete(created_entry.id)
    end
    
      # this test creates a data entry, calls serve action to get file url and test file content.
      should "get the file url with data content when the forceProxy is 0" do

        data_entry = Kaltura::KalturaDataEntry.new
        data_entry.name = "kaltura_test"
        data_entry.data_content = @content

        created_entry = @client.data_service.add(data_entry)

        assert_not_nil created_entry.id

        file_url = @client.data_service.serve(created_entry.id, nil, false)
      
        uri = URI.parse(file_url)
        http = Net::HTTP.new(uri.host, uri.port)
        http.use_ssl = (uri.scheme == 'https')
        http.verify_mode = OpenSSL::SSL::VERIFY_NONE
        request = Net::HTTP::Get.new(uri.request_uri)
        response = http.request(request)
        assert_true (response.class == Net::HTTPFound || response.class == Net::HTTPOK)

        assert_nil @client.data_service.delete(created_entry.id)
    end
  
    def setup
       super
       @content =
        <<-TXT
          Test data content.
          TXT
       end
end