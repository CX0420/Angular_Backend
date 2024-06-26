﻿using System;
namespace AngularBackend.Models;
using System.ComponentModel.DataAnnotations;

public class CustomerModel
{
	[Key]
	public int CustomerId { get; set; }
	public string CustomerName { get; set; }
	public int CustomerGender { get; set; }
	public string CustomerPhoneNumber { get; set; }
	public string CustomerEmail { get; set; }
	public int IsDeleted { get; set; }
	public DateTime CreatedTime { get; set; }
	public DateTime UpdatedTime { get; set; }
}

