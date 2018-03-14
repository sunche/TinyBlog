CREATE TABLE public."comment"
(
  id uuid NOT NULL,
  articleId uuid NOT NULL,
  text character varying NOT NULL,
  author character varying NOT NULL,
  CONSTRAINT comment_pkey PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."comment"
  OWNER TO postgres;