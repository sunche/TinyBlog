CREATE TABLE public."comment"
(
  id uuid NOT NULL,
  articleId uuid NOT NULL,
  text character varying,
  userName character varying,
  CONSTRAINT comment_pkey PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."comment"
  OWNER TO postgres;