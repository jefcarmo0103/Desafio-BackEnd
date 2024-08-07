-- drop TABLE public.RentPlan
-- drop TABLE public.Motorcyle
-- drop TABLE public.TypeCnh
-- drop TABLE public.Deliveryman
-- drop TABLE public.Rent

CREATE TABLE public.RentPlan (
    RentPlanUdf UUID NOT NULL,
    Description VARCHAR(200) NOT NULL,
    QuantityDays INT NOT NULL,
    Price DECIMAL(10,2),
    HasFeeForAnticipation BOOLEAN NOT NULL,
    AnticipationFeePercentage DECIMAL(10,2),    
    DelayFeePrice DECIMAL(10,2) NOT NULL DEFAULT 50.00,  
    Active BOOLEAN NOT NULL,
    CONSTRAINT PK_RentPlanUdf PRIMARY KEY(RentPlanUdf)
);


CREATE TABLE public.Motorcycle(
    MotorcycleUdf UUID NOT NULL,
    Plate CHAR(7) UNIQUE NOT NULL,
    Year INT NOT NULL,
    Model VARCHAR(100) NOT NULL,
    CONSTRAINT PK_MotorcycleUdf PRIMARY KEY(MotorcycleUdf)
);


CREATE TABLE public.Motorcycle2024(
    Motorcycle2024Udf UUID NOT NULL,
    MotorcycleUdf UUID NOT NULL,
    CONSTRAINT PK_Motorcycle2024Udf PRIMARY KEY(Motorcycle2024Udf),
    CONSTRAINT FK_MotorcycleUdf_Motorcycle2024 FOREIGN KEY(MotorcycleUdf) REFERENCES public.motorcycle(MotorcycleUdf)
);


CREATE TABLE public.TypeCnh(
    TypeCnhUdf UUID NOT NULL,
    Description VARCHAR(100) NOT NULL,
    QualifiedRentMotorcyle BOOLEAN NOT NULL,
    Active BOOLEAN NOT NULL,
    CONSTRAINT PK_TypeCnhUdf PRIMARY KEY(TypeCnhUdf)
);


CREATE TABLE public.Deliveryman(
    DeliverymanUdf UUID NOT NULL,
    Name VARCHAR(100) NOT NULL,
    CNPJ CHAR(14) UNIQUE NOT NULL,
    BirthdayDate TIMESTAMP NOT NULL,
    NumberCnh BIGINT NOT NULL,
    TypeCnhUdf UUID NOT NULL,
    ImageCnh VARCHAR(120) NOT NULL,
    CONSTRAINT PK_DeliverymanUdf PRIMARY KEY(DeliverymanUdf),
    CONSTRAINT FK_TypeCnhUdf_Deliveryman FOREIGN KEY(TypeCnhUdf) REFERENCES public.TypeCnh(TypeCnhUdf)
);

CREATE TABLE public.Rent(
    RentUdf UUID NOT NULL,
    RentPlanUdf UUID NOT NULL,
    DeliverymanUdf UUID NOT NULL,
    MotorcycleUdf UUID NOT NULL,
    BeginDate TIMESTAMP NOT NULL,
    ExpectedEndDate TIMESTAMP NOT NULL,
    EndDate TIMESTAMP,
    FinalPrice DECIMAL(10,2),
    CreationDate TIMESTAMP NOT NULL,
    CONSTRAINT PK_RentUdf PRIMARY KEY(RentUdf),
    CONSTRAINT FK_RentPlanUdf_Rent FOREIGN KEY(RentPlanUdf) REFERENCES public.rentPlan(RentPlanUdf),
    CONSTRAINT FK_DeliverymanUdf_Rent FOREIGN KEY(DeliverymanUdf) REFERENCES public.deliveryman(DeliverymanUdf),
    CONSTRAINT FK_MotorcycleUdf_Rent FOREIGN KEY(MotorcycleUdf) REFERENCES public.motorcycle(MotorcycleUdf)
);


INSERT INTO public.TypeCnh VALUES (gen_random_uuid(), 'A', TRUE, TRUE);
INSERT INTO public.TypeCnh VALUES (gen_random_uuid(), 'B', FALSE, TRUE);
INSERT INTO public.TypeCnh VALUES (gen_random_uuid(), 'A + B', TRUE, TRUE); 

INSERT INTO public.RentPlan VALUES(gen_random_uuid(), '7 dias com um custo de R$30,00 por dia', 7, 30.00, TRUE, 20.00, 50.00, TRUE);
INSERT INTO public.RentPlan VALUES(gen_random_uuid(), '15 dias com um custo de R$28,00 por dia', 15, 28.00, TRUE, 40.00, 50.00, TRUE);
INSERT INTO public.RentPlan VALUES(gen_random_uuid(), '30 dias com um custo de R$22,00 por dia', 30, 22.00, FALSE, NULL, 50.00, TRUE);
INSERT INTO public.RentPlan VALUES(gen_random_uuid(), '45 dias com um custo de R$20,00 por dia', 45, 20.00, FALSE, NULL, 50.00, TRUE);
INSERT INTO public.RentPlan VALUES(gen_random_uuid(), '50 dias com um custo de R$18,00 por dia', 50, 18.00, FALSE, NULL, 50.00, TRUE);

