import http from 'k6/http';
import { check, sleep } from 'k6';


export default function () {
	const url = 'https://localhost:44316/api/User/Login';

	const payload = JSON.stringify({
		"userName":"8888881",
		"password":"9999991"
	});

	const params = {
		headers: {
			'Content-Type': 'application/json',
		},
	};

	http.post(url, payload, params);
}




