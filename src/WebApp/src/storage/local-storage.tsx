import CryptoJS from 'crypto-js'

const secretKey = '123456'

const encrypt = (plainText: string) => {
  const cipherText = CryptoJS.AES.encrypt(plainText, secretKey).toString()
  return cipherText
}

const decrypt = (cipherText: string) => {
  const bytes = CryptoJS.AES.decrypt(cipherText, secretKey)
  const plainText = bytes.toString(CryptoJS.enc.Utf8)
  return plainText
}

export function SetLocalStorage(key: string, value: string) {
  const encryptedValue = encrypt(value)
  localStorage.setItem(key, encryptedValue)
}

export function GetLocalStorage(key: string) {
  const encryptedValue = localStorage.getItem(key)
  if (!encryptedValue) {
    return null
  }

  const decryptedValue = decrypt(encryptedValue)

  return decryptedValue
}
