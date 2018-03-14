CREATE TABLE public."article"
(
  id uuid NOT NULL,
  text character varying NOT NULL,
  title character varying NOT NULL,
  author character varying NOT NULL,
  CONSTRAINT article_pkey PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."article"
  OWNER TO postgres;