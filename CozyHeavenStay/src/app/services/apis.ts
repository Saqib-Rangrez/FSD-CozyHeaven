import { API_URL } from '../../environments/environment';

const BASE_URL : string = API_URL;

// AUTH ENDPOINTS
export const endpoints = {
    SIGNUP_USER_API: BASE_URL + "/Auth/User/Register",
    SIGNUP_OWNER_API: BASE_URL + "/Auth/HotelOwner/Register",
    SIGNUP_ADMIN_API: BASE_URL + "/Auth/Admin/Register",
    LOGIN_ADMIN_API: BASE_URL + "/Auth/Admin/Login",
    LOGIN_OWNER_API: BASE_URL + "/Auth/HotelOwner/Login",
    LOGIN_USER_API: BASE_URL + "/Auth/User/Login",
    LOGOUT_API: BASE_URL + "/Auth/Logout",
    FORGETPASSWORD_API: BASE_URL + "/Auth/ForgetPassword",
    RESETPASSWORD_API: BASE_URL + "/Auth/ResetPassword",
}

// USER ENDPOINTS
export const userEndpoints = {
    GET_ALL_USERS_API: BASE_URL + "/User/GetAllUsers",
    GET_USER_BY_ID_API: BASE_URL + "/User/GetUserById/",
    GET_USER_BY_EMAIL_API: BASE_URL + "/User/GetUserByEmail/",
    CREATE_USER_API: BASE_URL + "/User/CreateUser",
    UPDATE_USER_API: BASE_URL + "/User/UpdateUser",
    DELETE_USER_API: BASE_URL + "/User/DeleteUser/",
    UPLOAD_DISPLAY_PICTURE_API: BASE_URL + "/User/UploadDisplayPicture"    
}

// ADMIN ENDPOINTS
export const adminEndpoints = {
    GET_ALL_ADMINS_API: BASE_URL + "/Admin/GetAllAdmins",
    GET_ADMIN_BY_ID_API: BASE_URL + "/Admin/GetAdminById/",
    GET_ADMIN_BY_EMAIL_API: BASE_URL + "/Admin/GetAdminByEmail/",
    CREATE_ADMIN_API: BASE_URL + "/Admin/CreateAdmin",
    UPDATE_ADMIN_API: BASE_URL + "/Admin/UpdateAdmin",
    DELETE_ADMIN_API: BASE_URL + "/Admin/DeleteAdmin/"    
}

// OWNER ENDPOINTS
export const ownerEndpoints = {
    GET_ALL_HOTEL_OWNERS_API: BASE_URL + "/HotelOwner/GetAllHotelOwners",
    GET_HOTEL_OWNER_BY_ID_API: BASE_URL + "/HotelOwner/GetHotelOwnerById/",
    GET_HOTEL_OWNER_BY_EMAIL_API: BASE_URL + "/HotelOwner/GetHotelOwnerByEmail/",
    CREATE_HOTEL_OWNER_API: BASE_URL + "/HotelOwner/CreateHotelOwner",
    UPDATE_HOTEL_OWNER_API: BASE_URL + "/HotelOwner/UpdateHotelOwner",
    DELETE_HOTEL_OWNER_API: BASE_URL + "/HotelOwner/DeleteHotelOwner/"    
}


// REVIEW ENDPOINTS
export const reviewEndpoints = {
    ADD_REVIEW_API: BASE_URL + "/Review/AddReview",
    UPDATE_REVIEW_API: BASE_URL + "/Review/UpdateReview",
    DELETE_REVIEW_API: BASE_URL + "/Review/DeleteReview/",
    GET_ALL_REVIEWS_API: BASE_URL + "/Review/GetAllReviews",
    GET_REVIEW_BY_REVIEWID_API: BASE_URL + "/Review/GetReviewByReviewId/",
    GET_REVIEW_BY_USERID_API: BASE_URL + "/Review/GetReviewByUserId/",
    GET_REVIEW_BY_HOTELID_API: BASE_URL + "/Review/GetReviewByHotelId/",
}

// BOOKINGS ENDPOINTS
export const bookingsEndpoints = {
    ADD_BOOKING_API: BASE_URL + "/Booking/CreateBooking",
    UPDATE_BOOKINGBOOKING_API: BASE_URL + "/Booking/UpdateBooking",
    DELETE_BOOKING_API: BASE_URL + "/Booking/DeleteBooking/",
    GET_ALL_BOOKING_API: BASE_URL + "/Booking/GetAllBookings",
    GET_BOOKING_BY_BOOKINGID_API: BASE_URL + "/Booking/GetBookingById/",
    GET_BOOKING_BY_USERID_API: BASE_URL + "/Booking/GetBookingByUserId/"
}

// HOTEL ENDPOINTS
export const hotelEndpoints = {
    GET_ALL_HOTELS_API: BASE_URL + "/Hotel/GetAllHotels",
    SEARCH_HOTELS_API: BASE_URL + "/Hotel/SearchHotels",
    GET_HOTEL_BY_ID_API: BASE_URL + "/Hotel/GetHotelById/",
    GET_HOTEL_BY_NAME_API: BASE_URL + "/Hotel/GetHotelByName/",
    CREATE_HOTEL_API: BASE_URL + "/Hotel/CreateHotel",
    UPDATE_HOTEL_API: BASE_URL + "/Hotel/UpdateHotel",
    DELETE_HOTEL_API: BASE_URL + "/Hotel/DeleteHotel/"
};

// PAYMENT ENDPOINTS
export const paymentEndpoints = {
    GET_ALL_PAYMENTS_API: BASE_URL + "/Payment/GetAllPayments",
    GET_PAYMENT_BY_ID_API: BASE_URL + "/Payment/GetPaymentById/",
    GET_PAYMENT_BY_BOOKING_ID_API: BASE_URL + "/Payment/GetPaymentByBookingId/",
    CREATE_PAYMENT_API: BASE_URL + "/Payment/CreatePayment",
    UPDATE_PAYMENT_API: BASE_URL + "/Payment/UpdatePayment",
    DELETE_PAYMENT_API: BASE_URL + "/Payment/DeletePayment/"
};

// REFUND ENDPOINTS
export const refundEndpoints = {
    GET_ALL_REFUNDS_API: BASE_URL + "/Refund/GetAllRefunds",
    GET_REFUND_BY_ID_API: BASE_URL + "/Refund/GetRefundById/",
    GET_REFUND_BY_PAYMENT_ID_API: BASE_URL + "/Refund/GetRefundByPaymentId/",
    CREATE_REFUND_API: BASE_URL + "/Refund/CreateRefund",
    UPDATE_REFUND_API: BASE_URL + "/Refund/UpdateRefund",
    DELETE_REFUND_API: BASE_URL + "/Refund/DeleteRefund/",
    APPROVE_REFUND_API: BASE_URL + "/Refund/ApproveRefund/"
};


//ROOM ENDPOINTS
export const roomEndpoints = {
    GET_ALL_ROOMS_API: BASE_URL + "/Room/GetAllRooms",
    GET_ROOM_BY_ID_API: BASE_URL + "/Room/GetRoomById/",
    CREATE_ROOM_API: BASE_URL + "/Room/CreateRoom",
    UPDATE_ROOM_API: BASE_URL + "/Room/UpdateRoom",
    DELETE_ROOM_API: BASE_URL + "/Room/DeleteRoom/"
};
