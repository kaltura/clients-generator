#!/usr/bin/php
<?php
// ===================================================================================================
//                           _  __     _ _
//                          | |/ /__ _| | |_ _  _ _ _ __ _
//                          | ' </ _` | |  _| || | '_/ _` |
//                          |_|\_\__,_|_|\__|\_,_|_| \__,_|
//
// This file is part of the Kaltura Collaborative Media Suite which allows users
// to do with audio, video, and animation what Wiki platfroms allow them to do with
// text.
//
// Copyright (C) 2006-2011  Kaltura Inc.
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
// @ignore
// ===================================================================================================

require_once(dirname(__file__) . '/lib/KalturaCommandLineParser.php');
require_once(dirname(__file__) . '/lib/KalturaSession.php');

function formatTimeInterval($secs)
{
	$bit = array(
		' year'   => intval($secs / 31556926),
		' month'  => $secs / 2628000 % 12,
		' day'    => $secs / 86400 % 30,
		' hour'   => $secs / 3600 % 24,
		' minute' => $secs / 60 % 60,
		' second' => $secs % 60
	);

	foreach($bit as $k => $v)
	{
		if($v > 1)
		{
			$ret[] = $v . $k . 's';
		}
		else if($v == 1)
		{
			$ret[] = $v . $k;
		}
	}
	
	$ret = array_slice($ret, 0, 2);		// don't care about more than 2 levels
	array_splice($ret, count($ret) - 1, 0, 'and');

	return join(' ', $ret);
}

function formatKs($ksObj, $fieldNames)
{
	$printDelim = false;
	if (isset($fieldNames['hash']))
	{
		echo str_pad('Sig', 20) . $ksObj->hash . "\n";
		unset($fieldNames['hash']);
		$printDelim = true;
	}
	if (isset($fieldNames['real_str']))
	{
		echo str_pad('Fields', 20) . $ksObj->real_str . "\n";
		unset($fieldNames['real_str']);
		$printDelim = true;
	}
	if ($printDelim)
	{
		echo "---\n";
	}
	
	foreach ($fieldNames as $fieldName)
	{
		echo str_pad($fieldName, 20) . $ksObj->$fieldName;
		if ($fieldName == 'valid_until')
		{
			$currentTime = time();
			echo ' = ' . date('Y-m-d H:i:s', $ksObj->valid_until);
			if ($currentTime >= $ksObj->valid_until)
			{
				echo ' (expired ' . formatTimeInterval($currentTime - $ksObj->valid_until) . ' ago';
			}
			else
			{
				echo ' (will expire in ' . formatTimeInterval($ksObj->valid_until - $currentTime);
			}
			echo ')';
		}
		echo "\n";
	}
}

function formatKsTable($ksObj, $fieldNames)
{
	$result = array();
	foreach ($fieldNames as $fieldName)
	{
		$result[] = $ksObj->$fieldName;
	}
	echo implode("\t", $result) . "\n";
}

$commandLineSwitches = array(
	array(KalturaCommandLineParser::SWITCH_NO_VALUE, 'i', 'stdin', 'Read input from stdin'),
	array(KalturaCommandLineParser::SWITCH_NO_VALUE, 'p', 'partner-id', 'Print the partner id'),
	array(KalturaCommandLineParser::SWITCH_NO_VALUE, 't', 'type', 'Print the session type'),
	array(KalturaCommandLineParser::SWITCH_NO_VALUE, 'u', 'user', 'Print the user name'),
	array(KalturaCommandLineParser::SWITCH_NO_VALUE, 'e', 'expiry', 'Print the session expiry'),
	array(KalturaCommandLineParser::SWITCH_NO_VALUE, 'v', 'privileges', 'Print the privileges'),
);

// parse command line
$options = KalturaCommandLineParser::parseArguments($commandLineSwitches);
$arguments = KalturaCommandLineParser::stripCommandLineSwitches($commandLineSwitches, $argv);

KalturaSecretRepository::init();

if (!$arguments && !isset($options['stdin']))
{
	$usage = "Usage: extractKs [switches] [<ks>]\nOptions:\n";
	$usage .= KalturaCommandLineParser::getArgumentsUsage($commandLineSwitches);
	die($usage);
}

$fieldNames = array();
if (isset($options['partner-id']))
{
	$fieldNames[] = 'partner_id';
}
if (isset($options['type']))
{
	$fieldNames[] = 'type';
}
if (isset($options['user']))
{
	$fieldNames[] = 'user';
}
if (isset($options['expiry']))
{
	$fieldNames[] = 'valid_until';
}
if (isset($options['privileges']))
{
	$fieldNames[] = 'privileges';
}
if (!$fieldNames)
{
	$fieldNames = array('hash','real_str','partner_id','partner_pattern','valid_until','type','rand','user','privileges','master_partner_id','additional_data');
}

if (!isset($options['stdin']))
{
	$ks = reset($arguments);
	$ksObj = KalturaSession::getKsObject($ks);
	if (!$ksObj)
		die("Failed to parse ks {$ks}\n");
	formatKs($ksObj, $fieldNames);
	die;
}

$f = fopen('php://stdin', 'r');
for (;;)
{
	$line = fgets($f);
	if (!$line)
	{
		break;
	}
	$ks = trim($line);
	$ksObj = KalturaSession::getKsObject($ks);
	if ($ksObj)
	{
		formatKsTable($ksObj, $fieldNames);
	}
	else
	{
		echo "Failed to parse ks {$ks}\n";
	}
}
fclose($f);
