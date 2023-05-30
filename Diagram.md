                               +--------------+
                               |              |
                               |    Client    |
                               |              |
                               +------+-------+
                                      |
                                      |
                             1. HTTP Request
                                      |
                                      |
                             +--------+-------+
                             |                |
                     2. Authorization    Authentication
                             |                |
                      +------+-------+        |
                      |              |        |
                      |  Auth API    | <------+
                      |              |
                      +------+-------+
                             |
                             |
                 3. Access Token or Error Response
                             |
                             |
                     +-------+--------+
                     |                |
            4. Authorized API Request |
                     |                |
              +------+-------+        |
              |              |        |
              | Booking API  | <------+
              |              |
              +--------------+
                             |
                             |
                     5. HTTP Response
                             |
                             |
                     +-------+--------+
                     |                |
                6.   | Client Response|
                     |                |
                     +----------------+
