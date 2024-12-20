# All Results Pattern

## Table of Contents

1. [Introduction](#introduction-)
2. [Sample Requests](#used-request-body-)
3. [Example responses](#sample-responses-)
   1. [Ardalis](#using-ardalisresult)
   2. [Error-or](#using-erroror)
   3. [Fluent Results](#using-fluentresults)
   4. [CSharp Functional Extension](#using-csharpfunctionalextensions)
   5. [Custom Result Implementation](#using-customresult)
4. [Some Notes](#notes-)
5. [References](#references-)

-----
### Introduction:-
- In this simple project tried to showcase the *Result* pattern using different popular nuget packages available as well as custom implementation.
- Also how can we simplify and send consistent response from our API to consume from client.
-----

#### Used Request body:-
- InValid
```json
{
    "name": "John Doe",
    "age": 1,
    "email" : ""
}
```
- Valid
```json
{
    "name": "John Doe",
    "age": 21,
    "email": "test@test.com"
}
```
----
## Sample responses:-

### Using `Ardalis.Result`

1. When validation fails using `FluentValidation` and gets all the errors.
   ```json
   {
     "value": null,
     "status": 5,
     "isSuccess": false,
     "successMessage": "",
     "correlationId": "",
     "location": "",
     "errors": [],
     "validationErrors": [
       {
         "identifier": "Email",
         "errorMessage": "Email is required.",
         "errorCode": "NotEmptyValidator",
         "severity": 0
       },
       {
         "identifier": "Email",
         "errorMessage": "A valid email address is required.",
         "errorCode": "EmailValidator",
         "severity": 0
       },
       {
         "identifier": "Age",
         "errorMessage": "Age must be between 18 and 60.",
         "errorCode": "InclusiveBetweenValidator",
         "severity": 0
       }
     ]
   }
   ```

2. When we need to return single error.
   ```json
   {
     "value": null,
     "status": 6,
     "isSuccess": false,
     "successMessage": "",
     "correlationId": "",
     "location": "",
     "errors": [
        "Resource not found.", "Resource with Id 2 doesn't exist."
        ],
     "validationErrors": []
   }
   ```

3. When returns success result with some value.
   ```json
   {
     "value": {
       "name": "John Doe",
       "email": "test@test.com",
       "age": 21,
       "weatherForecasts": [
         {
           "date": "2024-12-21",
           "temperatureC": 52,
           "temperatureF": 125,
           "summary": "Warm"
         },
         {
           "date": "2024-12-22",
           "temperatureC": 8,
           "temperatureF": 46,
           "summary": "Balmy"
         }
       ]
     },
     "status": 0,
     "isSuccess": true,
     "successMessage": "",
     "correlationId": "",
     "location": "",
     "errors": [],
     "validationErrors": []
   }
   ```

-----

### Using `CSharpFunctionalExtensions`

1.  When validation fails using `FluentValidation` and gets all the errors.
    ```json
    [
        {
            "propertyName": "Email",
            "errorMessage": "Email is required.",
            "attemptedValue": "",
            "customState": null,
            "severity": 0,
            "errorCode": "NotEmptyValidator",
            "formattedMessagePlaceholderValues": {
                "PropertyName": "Email",
                "PropertyValue": "",
                "PropertyPath": "Email"
                }
        },
        {
        "propertyName": "Email",
        "errorMessage": "A valid email address is required.",
        "attemptedValue": "",
        "customState": null,
        "severity": 0,
        "errorCode": "EmailValidator",
            "formattedMessagePlaceholderValues": {
            "PropertyName": "Email",
            "PropertyValue": "",
            "PropertyPath": "Email"
            }
        },
        {
        "propertyName": "Age",
        "errorMessage": "Age must be between 18 and 60.",
        "attemptedValue": 1,
        "customState": null,
        "severity": 0,
        "errorCode": "InclusiveBetweenValidator",
        "formattedMessagePlaceholderValues": {
            "From": 18,
            "To": 60,
            "PropertyName": "Age",
            "PropertyValue": 1,
            "PropertyPath": "Age"
            }
        }
    ]
    ```

2.  When returns success result with some value.
    ```json
    {
      "name": "John Doe",
      "email": "test@test.com",
      "age": 21
    }
    ```

------

### Using `ErrorOr`

1.  When validation fails using `FluentValidation` and gets all the errors.
    ```json
    {
        "isError": true,
        "errors": [
            {
                "code": "General.Validation",
                "description": "A validation error has occurred.",
                "type": 2,
                "numericType": 2,
                "metadata": {
                    "Email": [
                        {
                            "propertyName": "Email",
                            "errorMessage": "Email is required.",
                            "attemptedValue": "",
                            "customState": null,
                            "severity": 0,
                            "errorCode": "NotEmptyValidator",
                            "formattedMessagePlaceholderValues": {
                                "PropertyName": "Email",
                                "PropertyValue": "",
                                "PropertyPath": "Email"
                                }
                        },
                        {
                            "propertyName": "Email",
                            "errorMessage": "A valid email address is required.",
                            "attemptedValue": "",
                            "customState": null,
                            "severity": 0,
                            "errorCode": "EmailValidator",
                            "formattedMessagePlaceholderValues": {
                                "PropertyName": "Email",
                                "PropertyValue": "",
                                "PropertyPath": "Email"
                                }
                        }
                    ],
                    "Age": [
                        {
                            "propertyName": "Age",
                            "errorMessage": "Age must be between 18 and 60.",
                            "attemptedValue": 1,
                            "customState": null,
                            "severity": 0,
                            "errorCode": "InclusiveBetweenValidator",
                            "formattedMessagePlaceholderValues": {
                                "From": 18,
                                "To": 60,
                                "PropertyName": "Age",
                                "PropertyValue": 1,
                                "PropertyPath": "Age"
                                }
                        }
                    ]
                }
            }
        ],
        "errorsOrEmptyList": [
            {
                "code": "General.Validation",
                "description": "A validation error has occurred.",
                "type": 2,
                "numericType": 2,
                "metadata": {
                    "Email": [
                    {
                        "propertyName": "Email",
                        "errorMessage": "Email is required.",
                        "attemptedValue": "",
                        "customState": null,
                        "severity": 0,
                        "errorCode": "NotEmptyValidator",
                        "formattedMessagePlaceholderValues": {
                            "PropertyName": "Email",
                            "PropertyValue": "",
                            "PropertyPath": "Email"
                        }
                    },
                    {
                        "propertyName": "Email",
                        "errorMessage": "A valid email address is required.",
                        "attemptedValue": "",
                        "customState": null,
                        "severity": 0,
                        "errorCode": "EmailValidator",
                        "formattedMessagePlaceholderValues": {
                            "PropertyName": "Email",
                            "PropertyValue": "",
                            "PropertyPath": "Email"
                            }
                        }
                    ],
                    "Age": [
                    {
                        "propertyName": "Age",
                        "errorMessage": "Age must be between 18 and 60.",
                        "attemptedValue": 1,
                        "customState": null,
                        "severity": 0,
                        "errorCode": "InclusiveBetweenValidator",
                        "formattedMessagePlaceholderValues": {
                            "From": 18,
                            "To": 60,
                            "PropertyName": "Age",
                            "PropertyValue": 1,
                            "PropertyPath": "Age"
                            }
                        }
                    ]
                }
            }
        ],
        "value": null,
        "firstError": {
            "code": "General.Validation",
            "description": "A validation error has occurred.",
            "type": 2,
            "numericType": 2,
            "metadata": {
                "Email": [
                    {
                        "propertyName": "Email",
                        "errorMessage": "Email is required.",
                        "attemptedValue": "",
                        "customState": null,
                        "severity": 0,
                        "errorCode": "NotEmptyValidator",
                        "formattedMessagePlaceholderValues": {
                            "PropertyName": "Email",
                            "PropertyValue": "",
                            "PropertyPath": "Email"
                            }
                    },
                    {
                        "propertyName": "Email",
                        "errorMessage": "A valid email address is required.",
                        "attemptedValue": "",
                        "customState": null,
                        "severity": 0,
                        "errorCode": "EmailValidator",
                        "formattedMessagePlaceholderValues": {
                            "PropertyName": "Email",
                            "PropertyValue": "",
                            "PropertyPath": "Email"
                            }
                    }
                ],
                "Age": [
                    {
                    "propertyName": "Age",
                    "errorMessage": "Age must be between 18 and 60.",
                    "attemptedValue": 1,
                    "customState": null,
                    "severity": 0,
                    "errorCode": "InclusiveBetweenValidator",
                    "formattedMessagePlaceholderValues": {
                        "From": 18,
                        "To": 60,
                        "PropertyName": "Age",
                        "PropertyValue": 1,
                        "PropertyPath": "Age"
                        }
                    }
                ]
            }
        }
    }
    ```

2.  When we need to return single error.
```json
{
    "isError": true,
    "errors": [
        {
            "code": "General.Unexpected",
            "description": "Something unexpected happened.",
            "type": 1,
            "numericType": 1,
            "metadata": null
        }
    ],
    "errorsOrEmptyList": [
        {
            "code": "General.Unexpected",
            "description": "Something unexpected happened.",
            "type": 1,
            "numericType": 1,
            "metadata": null
        }
    ],
    "value": null,
    "firstError": {
        "code": "General.Unexpected",
        "description": "Something unexpected happened.",
        "type": 1,
        "numericType": 1,
        "metadata": null
    }
}
```

3.  When returns success result with some value.
```json
{
    "isError": false,
    "errors": [
        {
        "code": "ErrorOr.NoErrors",
        "description": "Error list cannot be retrieved from a successful ErrorOr.",
        "type": 1,
        "numericType": 1,
        "metadata": null
        }
    ],
    "errorsOrEmptyList": [],
    "value": {
        "name": "John Doe",
        "email": "test@test.com",
        "age": 21,
        "weatherForecasts": [
            {
                "date": "2024-12-21",
                "temperatureC": 25,
                "temperatureF": 76,
                "summary": "Freezing"
            },
            {
                "date": "2024-12-22",
                "temperatureC": -15,
                "temperatureF": 6,
                "summary": "Bracing"
            }
        ]
    },
    "firstError": {
        "code": "ErrorOr.NoFirstError",
        "description": "First error cannot be retrieved from a successful ErrorOr.",
        "type": 1,
        "numericType": 1,
        "metadata": null
    }
}
```

------

### Using `FluentResults`

1.  When validation fails using `FluentValidation` and gets all the errors.
```json
[
    {
        "reasons": [],
        "message": "Some validation errors.",
        "metadata": {
            "Email": [
                {
                    "propertyName": "Email",
                    "errorMessage": "Email is required.",
                    "attemptedValue": "",
                    "customState": null,
                    "severity": 0,
                    "errorCode": "NotEmptyValidator",
                    "formattedMessagePlaceholderValues": {
                        "PropertyName": "Email",
                        "PropertyValue": "",
                        "PropertyPath": "Email"
                    }
                },
                {
                    "propertyName": "Email",
                    "errorMessage": "A valid email address is required.",
                    "attemptedValue": "",
                    "customState": null,
                    "severity": 0,
                    "errorCode": "EmailValidator",
                    "formattedMessagePlaceholderValues": {
                        "PropertyName": "Email",
                        "PropertyValue": "",
                        "PropertyPath": "Email"
                    }
                }
            ],
            "Age": [
                {
                    "propertyName": "Age",
                    "errorMessage": "Age must be between 18 and 60.",
                    "attemptedValue": 1,
                    "customState": null,
                    "severity": 0,
                    "errorCode": "InclusiveBetweenValidator",
                    "formattedMessagePlaceholderValues": {
                        "From": 18,
                        "To": 60,
                        "PropertyName": "Age",
                        "PropertyValue": 1,
                        "PropertyPath": "Age"
                    }
                }
            ]
        }
    }
]
```

2.  When we need to return single error.
```json
[
  {
    "reasons": [],
    "message": "Some unexpected happened.",
    "metadata": {}
  }
]
```

3.  When returns success result with some value.
```json
{
    "valueOrDefault": {
        "name": "John Doe",
        "email": "test@test.com",
        "age": 21,
        "weatherForecasts": [
        {
            "date": "2024-12-21",
            "temperatureC": 51,
            "temperatureF": 123,
            "summary": "Cool"
        },
        {
            "date": "2024-12-22",
            "temperatureC": 16,
            "temperatureF": 60,
            "summary": "Chilly"
        }
        ]
    },
    "value": {
        "name": "John Doe",
        "email": "test@test.com",
        "age": 21,
        "weatherForecasts": [
            {
                "date": "2024-12-21",
                "temperatureC": 51,
                "temperatureF": 123,
                "summary": "Cool"
            },
            {
                "date": "2024-12-22",
                "temperatureC": 16,
                "temperatureF": 60,
                "summary": "Chilly"
            }
        ]
    },
    "isFailed": false,
    "isSuccess": true,
    "reasons": [],
    "errors": [],
    "successes": []
}
```

-------
### Using `CustomResult` 
1.  When validation fails using `FluentValidation` and gets all the errors.
```json
{
  "value": null,
  "isSuccess": false,
  "message": "Request unsuccessful.",
  "error": {
    "code": "Request.validation",
    "description": "Sample request is invalid.",
    "errorType": 3,
    "metadata": {
      "Email": [
        {
          "propertyName": "Email",
          "errorMessage": "Email is required.",
          "attemptedValue": "",
          "customState": null,
          "severity": 0,
          "errorCode": "NotEmptyValidator",
          "formattedMessagePlaceholderValues": {
            "PropertyName": "Email",
            "PropertyValue": "",
            "PropertyPath": "Email"
          }
        },
        {
          "propertyName": "Email",
          "errorMessage": "A valid email address is required.",
          "attemptedValue": "",
          "customState": null,
          "severity": 0,
          "errorCode": "EmailValidator",
          "formattedMessagePlaceholderValues": {
            "PropertyName": "Email",
            "PropertyValue": "",
            "PropertyPath": "Email"
          }
        }
      ],
      "Age": [
        {
          "propertyName": "Age",
          "errorMessage": "Age must be between 18 and 60.",
          "attemptedValue": 1,
          "customState": null,
          "severity": 0,
          "errorCode": "InclusiveBetweenValidator",
          "formattedMessagePlaceholderValues": {
            "From": 18,
            "To": 60,
            "PropertyName": "Age",
            "PropertyValue": 1,
            "PropertyPath": "Age"
          }
        }
      ]
    }
  },
  "errors": [],
  "hasMultipleErrors": false
}
```

2.  When we need to return single error.
```json
{
  "value": null,
  "isSuccess": false,
  "message": "Request unsuccessful.",
  "error": {
    "code": "NotFound",
    "description": "A 'Not Found' error has occurred.",
    "errorType": 5,
    "metadata": null
    },
  "errors": [],
  "hasMultipleErrors": false
}
```

3.  When we need to return multiple errors.
```json
{
  "value": null,
  "isSuccess": false,
  "message": "Request unsuccessful with multiple errors.",
  "error": null,
  "errors": [
    {
      "code": "Request.Validation",
      "description": "Sample request is invalid.",
      "errorType": 3,
      "metadata": null
    },
    {
      "code": "NotFound",
      "description": "A 'Not Found' error has occurred.",
      "errorType": 5,
      "metadata": null
    },
    {
      "code": "Duplicate",
      "description": "A conflict error has occurred.",
      "errorType": 4,
      "metadata": null
    }
  ],
  "hasMultipleErrors": true
}
```

4.  When returns success result with some value.
```json
{
  "value": {
    "name": "John Doe",
    "email": "test@test.com",
    "age": 21,
    "weatherForecasts": [
      {
        "date": "2024-12-21",
        "temperatureC": 41,
        "temperatureF": 105,
        "summary": "Mild"
      },
      {
        "date": "2024-12-22",
        "temperatureC": 25,
        "temperatureF": 76,
        "summary": "Balmy"
      }
    ]
  },
  "isSuccess": true,
  "message": "Request successful.",
  "error": null,
  "errors": [],
  "hasMultipleErrors": false
}
```

----
#### NOTES:-
> - I have tried only some of the available nuget packages along with custom . One of them I missed is `LanguageExt.Core`.
> - Also all the packages has their own way of returning results , values or errors , so I just tried to implement the basic success and failure with `FluentValidation` errors.
> - The `CustomResult` type is almost similar like `ErrorOr` , but implemented as one of my need.
> - There are lot of customizations and improvements can be done according to our requirements.

----

-----
### References:-
- [Fluent Results](https://github.com/altmann/FluentResults)
- [Error-or](https://github.com/amantinband/error-or)
- [Ardalis Result](https://github.com/ardalis/result)
- [Language-Ext](https://github.com/louthy/language-ext)
- [CSharp Functional Extension](https://github.com/vkhorikov/CSharpFunctionalExtensions/)
-----