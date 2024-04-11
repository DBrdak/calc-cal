export const MAX_WEIGHT = 100000
export const MIN_WEIGHT = 0

export const USERNAME_PATTERN = RegExp(`^[a-zA-Z_.-]{1,50}$`)

export const PASSWORD_PATTERN = RegExp(`^(?=.*[!@#$%^&*()-_=+{};:',.<>?])(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,}$`)

export const NAME_PATTERN = RegExp('^[a-zA-Z]{1,50}$')

export const PHONE_NUMBER_PATTERN = RegExp('^[0-9]{8,11}$')

export const COUNTRY_CODE_PATTERN = RegExp(`^[+][0-9]{1,3}$`)