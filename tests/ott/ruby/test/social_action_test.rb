require 'test_helper'
require 'Kaltura'

class SocialActionTest < Test::Unit::TestCase
  
  # this test validates the session id 
  should "add social action" do
    
    social_action = Kaltura::KalturaSocialAction.new
    social_action.action_type = 'LIKE'
    social_action.time = Time.now.to_i
    social_action.asset_id = @media_asset_id
    social_action.asset_type = 'media'
    social_action.url = ''
    response = @client.social_action_service.add(social_action)
    @social_id = response.social_action.id

    assert_not_nil @social_id

  end

  should "delete social action" do
  
    social_action_filter = Kaltura::KalturaSocialActionFilter.new
    social_action_filter.asset_id_in = @media_asset_id
    social_action_filter.asset_type_equal = 'media'
    social_action_filter.action_type_in = 'LIKE'
    response = @client.social_action_service.list(social_action_filter)
	
    assert_not_nil response
	
	social_id = response.objects[0].id
    response = @client.social_action_service.delete(social_id)

    assert_nil response
  end
  
end