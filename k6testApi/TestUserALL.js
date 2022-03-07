import http from 'k6/http';
import { check, sleep } from 'k6';

/*
	登陆获取 Token
		创建用户信息 返回 UserKeyId
			查询用户信息
			修改用户信息 (名字后面加一个Up)
			删除用户信息（IsDelete 修改成 1）
 */

function GetLoginUserInfo() {
	var data = [{"UserName":"123", "PassWord":"1234"},{"UserName":"qas", "PassWord":"1111111"},{"UserName":"8888881", "PassWord":"9999991"},{"UserName":"2222222", "PassWord":"1111111"},{"UserName":"QIAO", "PassWord":"111111"},{"UserName":"Admain", "PassWord":"111111"},{"UserName":"Core", "PassWord":"111111"},{"UserName":"Core2022", "PassWord":"111111"},{"UserName":"张三", "PassWord":"111111"},{"UserName":"222333", "PassWord":"111111"}]
	for(var i = 0; i< 1000; i++){
		var d;
		do {
	    	d = data[Math.ceil(Math.random()*10)+0]
		}
		while (d == undefined);
		return d;
	}
}

function GetRandom() {
	return Math.ceil(Math.random()*100000) + 0
}

function GetUrl(url) {
	return 'https://localhost:44316/api/user/' + url;
}

function TestFindUser(UserKeyId, UserName, PassWord, Params) {
	const FindUserUrl = GetUrl('FindUser')

	const FindUserReq = JSON.stringify({
		"KeyId": UserKeyId
	});

	const FindUserRes = http.post(FindUserUrl, FindUserReq, Params);

	check(FindUserRes, {
		'FindUser.200 (成功)': (r) => r.status === 200
	});


	if (FindUserRes.status === 200) {

		var FindUserJson = JSON.parse(FindUserRes.body);

		check(FindUserJson, {
			'FindUser.body.status': (r) => r.status === 1,
			'FindUser.body.info': (r) => r.info === '操作成功',
			'FindUser.body.data not null': (r) => r.data != null,
		});

		if (FindUserJson.data != null) {
			const FindUserDataJson = JSON.stringify(FindUserJson.data)
			check(FindUserJson, {
				'FindUser.body.data.keyId': (r) => r.data.keyId === UserKeyId,
				'FindUser.body.data.userName': (r) => r.data.userName === UserName,
				'FindUser.body.data.passWord': (r) => r.data.passWord === PassWord,
			});
		}
	} else {
		//console.log(JSON.stringify(FindUserRes))
	}
}

function TestUpdateUser(UserKeyId, UserName, PassWord, Params) {

	const UpdateUserUrl = GetUrl('UpdateUser')

	const UpdateUserReq = JSON.stringify({
		"KeyId": UserKeyId,
		"userName": UserName,
		"password": PassWord
	});

	const UpdateUserRes = http.post(UpdateUserUrl, UpdateUserReq, Params);

	check(UpdateUserRes, {
		'UpdateUser.200 (成功)': (r) => r.status === 200
	});
	if (UpdateUserRes.status === 200) {

		var UpdateUserJson = JSON.parse(UpdateUserRes.body);

		// 新增成功
		if (UpdateUserJson.status === 1) {
			check(UpdateUserJson, {
				'UpdateUser.body.OK': (r) => r.status === 1,
				'UpdateUser.body.data.info': (r) => r.info == '操作成功',
				'UpdateUser.body.data.msg': (r) => r.msg  == null,
				'UpdateUser.body.data.data': (r) => r.data == true
			});
			return true;
		}
		// 新增失败
		else if (UpdateUserJson.status === 0) {
			check(UpdateUserJson, {
				'UpdateUser.body.NO': (r) => r.status === 0,
				'UpdateUser.body.data.info': (r) => r.info == '操作失败',
				'UpdateUser.body.data.data': (r) => r.data == false
			});
			//console.log(UpdateUserJson.msg)
			if (UpdateUserJson.msg == '不存在该用户') {
				check(UpdateUserJson, {
					'UpdateUser.body.data.不存在该用户': (r) => r.msg  == "不存在该用户" ,
				});
			} else if (UpdateUserJson.msg == '修改的账号密码与原账号密码一致') {
				check(UpdateUserJson, {
					'UpdateUser.body.data.修改的账号密码与原账号密码一致': (r) => r.msg  == "修改的账号密码与原账号密码一致" ,
				});
			} else {
				check(UpdateUserJson, {
					'UpdateUser.body.data.未知错误': (r) => r.msg  == "未知错误" ,
				});
			}
		}
		// 其他问题
		else {
			check(UpdateUserJson, {
				'UpdateUser.body.错误': (r) => r.status === 0
			});
		}
		// UpdateUser END
	} // UpdateUserRes.status === 200 END
	return false
}; // END TestUpdateUser

function TestDeleteUser(UserKeyId, UserName, PassWord, Params) {
	
	const DeleteUserUrl = GetUrl('DeleteUser')

	const DeleteUserReq = JSON.stringify({
		"KeyId": UserKeyId,
		"userName": UserName,
		"password": PassWord
	});

	const DeleteUserRes = http.post(DeleteUserUrl, DeleteUserReq, Params);

	check(DeleteUserRes, {
		'DeleteUser.200 (成功)': (r) => r.status === 200
	});
	if (DeleteUserRes.status === 200) {

		var DeleteUserJson = JSON.parse(DeleteUserRes.body);

		// 新增成功
		if (DeleteUserJson.status === 1) {
			check(DeleteUserJson, {
				'DeleteUser.body.OK': (r) => r.status === 1,
				'DeleteUser.body.data.info': (r) => r.info == '操作成功',
				'DeleteUser.body.data.msg': (r) => r.msg  == null,
				'DeleteUser.body.data.data': (r) => r.data == true
			});
		}
		// 新增失败
		else if (DeleteUserJson.status === 0) {
			check(DeleteUserJson, {
				'DeleteUser.body.NO': (r) => r.status === 0,
				'DeleteUser.body.data.info': (r) => r.info == '操作失败',
				'DeleteUser.body.data.不存在该用户': (r) => r.msg  == "不存在该用户",
				'DeleteUser.body.data.data': (r) => r.data == false
			});
		}
		// 其他问题
		else {
			check(DeleteUserJson, {
				'DeleteUser.body.错误': (r) => r.status === 0
			});
		}
		// DeleteUser END
	} // DeleteUserRes.status === 200 END
}; // END TestDeleteUser


function TestLogin(UserName, PassWord, Params) {
	//  Login Start
	const LoginURL = GetUrl('Login')
	// 请求参数
	const LoginReq = JSON.stringify({
		"userName": UserName,
		"password": PassWord
	});
	// 发送 POST 请求
	const LoginRes = http.post(LoginURL, LoginReq, Params);
	// 验证请求是否成功
	check(LoginRes, {
		'Login.200 (成功)': (r) => r.status === 200
	});

	if (LoginRes.status === 200) {
		// 获取到 返回的数据
		var LoginJson = JSON.parse(LoginRes.body);
		// 验证接口返回的参数是否符合预期
		if (LoginJson.status === 1) {
			check(LoginJson, {
				'Login.body.OK': (r) => r.status === 1,
				'Login.body.data not \'\'': (r) => r.data != '',
				'Login.body.data not null': (r) => r.data != null,
			});
			return true;
		} else if(LoginJson.status === 0) {
			check(LoginJson, {
				'Login.body.NO': (r) => r.status === 0,
				'Login.body.操作失败': (r) => r.info == "操作失败",
				'Login.body.data': (r) => r.data == null,
			});
			if (LoginJson.msg == '账号密码错误') {
				check(LoginJson, {
					'Login.body.账号密码错误': (r) => r.msg == "账号密码错误"
				});
			} else if (LoginJson.msg == '该账号已经被删除') {
				check(LoginJson, {
					'Login.body.该账号已经被删除': (r) => r.msg == "该账号已经被删除"
				});
			} else {
				check(LoginJson, {
					'Login.body.未知错误': (r) => r.msg == "未知错误"
				});
			}
		} else {
			check(LoginJson, {
				'Login.body.错误': (r) => r.status === 0
			});
		}
	}
	return false;
}; // END TestLogin

function TestCreateUser(UserName, PassWord, Params) {
	const CreateUserUrl = GetUrl('CreateUser')

	const CreateUserReq = JSON.stringify({
		"userName": UserName,
		"password": PassWord
	});

	const CreateUserRes = http.post(CreateUserUrl, CreateUserReq, Params);

	check(CreateUserRes, {
		'Login.200 (成功)': (r) => r.status === 200
	});
	if (CreateUserRes.status === 200) {

		var CreateUserJson = JSON.parse(CreateUserRes.body);

		// 新增成功
		if (CreateUserJson.status === 1) {
			check(CreateUserJson, {
				'CreateUser.body.OK': (r) => r.status === 1,
				'CreateUser.body.data not Empty': (r) => r.data != '00000000-0000-0000-0000-000000000000',
			});
			return { flag: true, "KeyId": CreateUserJson.data }
		}
		// 新增失败
		else if (CreateUserJson.status === 0) {
			check(CreateUserJson, {
				'CreateUser.body.NO': (r) => r.status === 0,
				'CreateUser.body.data Empty \'\'': (r) => r.data == '00000000-0000-0000-0000-000000000000',
			});
			if (CreateUserJson.msg == '账号已存在') {
				check(CreateUserJson, {
					'CreateUser.body.账号已存在': (r) => r.msg == "账号已存在"
				});
			} else {
				check(CreateUserJson, {
					'CreateUser.body.未知错误': (r) => r.msg == "未知错误"
				});
			}
		}
		// 其他问题
		else {
			check(CreateUserJson, {
				'CreateUser.body.错误': (r) => r.status === 0
			});
		}
	};
	// CreateUserRes.status === 200 END
	return { flag: false }
}; // END TestCreateUser


export default function () {

	const Params = {
		headers: {
			'Content-Type': 'application/json',
		},
	};
	// 随机获取一个账号
	var LoginUser = GetLoginUserInfo();
	// 随机获取一个数字
	var random = GetRandom();
	var UserName = LoginUser.UserName;
	var PassWord = LoginUser.PassWord;

	// 登陆
	var flagLogin = TestLogin(UserName, PassWord, Params)
	// 登陆成功
	if (flagLogin) {
		// 创建用户
		var UserName = LoginUser.UserName + random;
		var flagCreate = TestCreateUser(UserName, PassWord, Params)
		// 创建用户成功
		if (flagCreate.flag) {	
			const UserKeyId = flagCreate.KeyId;

			TestFindUser(UserKeyId, UserName, PassWord, Params)

			PassWord = PassWord + random + 'UP';
			var flagUpdate = TestUpdateUser(UserKeyId, UserName, PassWord, Params)
			if (flagUpdate) {
				TestDeleteUser(UserKeyId, UserName, PassWord, Params)
			}
		}
	}
}; // LoginRes.status === 200 END



/*

k6 run TestUserALL.js
k6 run --vus 10 --duration 30s TestUserALL.js
k6 run --vus 10 --duration 10s TestUserALL.js
k6 run --vus 20 --duration 5s TestUserALL.js
k6 run --vus 10 --duration 60s TestUserALL.js



 */