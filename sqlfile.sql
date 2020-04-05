use sys;
CREATE TABLE `POCusers` (
                        `id` int PRIMARY KEY,
                        `name` varchar(45),
                        `age` int,
                        `email` varchar(100)
                        );
                        INSERT INTO `sys`.`POCusers`
                        (`id`, `name`, `age`, `email`) 
                        VALUES ('1', 'Kunal Verma', '25', 'kunal.verma@nagarro.com');