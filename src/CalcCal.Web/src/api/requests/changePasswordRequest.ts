interface ChangePasswordRequest {
    username?: string | null
    countryCode?: string | null
    phoneNumber?: string | null
    newPassword: string
}