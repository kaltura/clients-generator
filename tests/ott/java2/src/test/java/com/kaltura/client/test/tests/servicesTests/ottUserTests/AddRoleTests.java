package com.kaltura.client.test.tests.servicesTests.ottUserTests;

import com.kaltura.client.services.OttUserService;
import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.types.ListResponse;
import com.kaltura.client.types.OTTUser;
import com.kaltura.client.types.UserRole;
import com.kaltura.client.types.UserRoleFilter;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.util.List;

import static com.kaltura.client.test.utils.OttUserUtils.generateOttUser;

public class AddRoleTests extends BaseTest {
    private OTTUser user;

    @BeforeClass
    private void ottUser_addRole_tests_setup() {
        user = generateOttUser();

        // OttUser/action/register
        OttUserService.RegisterOttUserBuilder registerOttUserBuilder = OttUserService.register(partnerId, user, defaultUserPassword);
        registerOttUserBuilder.setKs(null);
        Response<OTTUser> ottUserResponse = executor.executeSync(registerOttUserBuilder);
        user = ottUserResponse.results;
    }

    @Description("ottUser/action/addRole - addRole")
    @Test(enabled = false)
    // TODO: 3/27/2018 finish and fix test 
    private void addRole() {
        // set client
        client.setKs(getAdministratorKs());
        client.setUserId(Integer.valueOf(user.getId()));

        OttUserServiceImpl.addRole(client, 3);

        UserRoleFilter filter = new UserRoleFilter();
        filter.setIdIn(user.getId());

        client.setUserId(null);
        Response<ListResponse<UserRole>> userRoleListResponse = UserRoleServiceImpl.list(client, filter);
        List<UserRole> userRoles = userRoleListResponse.results.getObjects();

        for (UserRole userRole : userRoles) {
            System.out.println(userRole.getId().toString());
        }

    }
}
