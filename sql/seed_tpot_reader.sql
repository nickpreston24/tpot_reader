CREATE TABLE papers (
	id INTEGER PRIMARY KEY,
	title TEXT NOT NULL,
	tpot_url TEXT NOT NULL,
	status TEXT NOT NULL
);

insert into papers (id, title, tpot_url, status) values (1, 'What is Faith?', '', 'unread');

select * from papers;