export interface ChangePasswordRequest {
    verificationCode: string
    countryCode: string
    phoneNumber: string
    newPassword: string
}