
CREATE TABLE events (
                        id SERIAL PRIMARY KEY,
                        name VARCHAR(255) NOT NULL,
                        event_date DATE NOT NULL,
                        event_time VARCHAR(50) NOT NULL,
                        venue_id INTEGER NOT NULL references venues(id),
                        organizer_id INTEGER NOT NULL references organizers(id)
);

CREATE TABLE organizers (
                            id SERIAL PRIMARY KEY,
                            name VARCHAR(255) NOT NULL,
                            contact_info VARCHAR(255) NOT NULL
);

CREATE TABLE venues (
                        id SERIAL PRIMARY KEY,
                        name VARCHAR(255) NOT NULL,
                        address VARCHAR(255) NOT NULL,
                        capacity INT NOT NULL
);

