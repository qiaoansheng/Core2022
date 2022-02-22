import http from 'k6/http';
import { check, sleep } from 'k6';



/*
	登陆获取 Token
		创建用户信息 返回 UserKeyId
			查询用户信息
			修改用户信息
			删除用户信息
 */


export default function () {

	const Params = {
		headers: {
			'Content-Type': 'application/json',
		},
	};

	const UserName = 'Test123'
	const PassWord = '123'

	//  Login Start
	const LoginURL = 'https://localhost:44316/api/User/Login';
	// 请求参数
	const LoginReq = JSON.stringify({
		"userName":"8888881",
		"password":"9999991"
	});
	// 发送 POST 请求
	const LoginRes = http.post(LoginURL, LoginReq, Params);
	// 验证请求是否成功
	check(LoginRes, {
		'Login.status': (r) => r.status === 200
	});

	if (LoginRes.status === 200) {
		// 获取到 返回的数据
		var LoginJson = JSON.parse(LoginRes.body);
		// 验证接口返回的参数是否符合预期
		check(LoginJson, {
			'Login.body.status': (r) => r.status === 1,
			'Login.body.info': (r) => r.info === '操作成功',
			'Login.body.data': (r) => r.data == '6OhENiMRXic4IUEbhlNtW4EjhpDTBoRauJWRv9NKruUG+QamIghVoUyjYtkwjL3ubUWaW5BqqlCBubpuY7lH81K/U816WGdU0pXA2uwiGMY=',
			'Login.body.data not \'\'': (r) => r.data != '',
			'Login.body.data not null': (r) => r.data != null,
		});

		//  Login End

		//Params.headers['Cookie'] = LoginRes.Data

		const CreateUserUrl = 'https://localhost:44316/api/User/CreateUser';

		const CreateUserReq = JSON.stringify({
			"userName": UserName,
			"password": PassWord
		});

		const CreateUserRes = http.post(CreateUserUrl, CreateUserReq, Params);

		check(CreateUserRes, {
			'Login.status': (r) => r.status === 200
		});
		if (CreateUserRes.status === 200) {

			var CreateUserJson = JSON.parse(CreateUserRes.body);

			check(CreateUserJson, {
				'CreateUser.body.status': (r) => r.status === 1,
				'CreateUser.body.info': (r) => r.info === '操作成功',
				'CreateUser.body.data not \'\'': (r) => r.data != '00000000-0000-0000-0000-000000000000',
				'CreateUser.body.data not null': (r) => r.data != null,
			});

			const UserKeyId = CreateUserJson.data;
			//console.log('CreateUserKeyId = ' + UserKeyId);

			// CreateUser END
			const FindUserUrl = 'https://localhost:44316/api/user/FindUser'

			const FindUserReq = JSON.stringify({
				"KeyId": UserKeyId
			});

			const FindUserRes = http.post(FindUserUrl, FindUserReq, Params);

			check(FindUserRes, {
				'FindUser.status': (r) => r.status === 200
			});

			//console.log(JSON.stringify(FindUserRes))

			if (FindUserRes.status === 200) {

				var FindUserJson = JSON.parse(FindUserRes.body);
				//console.log(FindUserRes.body)

				check(FindUserJson, {
					'FindUser.body.status': (r) => r.status === 1,
					'FindUser.body.info': (r) => r.info === '操作成功',
					'FindUser.body.data not null': (r) => r.data != null,
				});

				if (FindUserJson.data != null) {
					const FindUserDataJson = JSON.stringify(FindUserJson.data)
					//console.log(FindUserJson.data)
					check(FindUserJson, {
						'FindUser.body.data.keyId': (r) => r.data.keyId === UserKeyId,
						'FindUser.body.data.userName': (r) => r.data.userName === UserName,
						'FindUser.body.data.passWord': (r) => r.data.passWord === PassWord,
					});
				}
			} else {
				//console.log(JSON.stringify(FindUserRes))
			}

/*



*/

			// FindUser END
			

		};// CreateUserRes.status === 200 END


	}; // LoginRes.status === 200 END






}


/*

k6 run TestUserALL.js
k6 run --vus 10 --duration 30s TestUserALL.js

 */