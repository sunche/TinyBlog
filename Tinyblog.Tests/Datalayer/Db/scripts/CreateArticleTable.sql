CREATE TABLE public."article"
(
  id uuid NOT NULL,
  text character varying,
  title character varying,
  CONSTRAINT article_pkey PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."article"
  OWNER TO postgres;