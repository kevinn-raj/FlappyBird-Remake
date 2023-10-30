@echo off
REM REMAKE
set env="Z:\Git\FlappyBird-Remake\Builds\FlappyBird-Remake.exe"
set run_name=run_3


:: Execute this
mlagents-learn PCG.yaml  --num-envs=7 --run-id=%run_name% --torch-device=cuda --time-scale=2 --no-graphics --torch-device=cuda --env=%env% --force

:: Execute this
REM mlagents-learn PCG.yaml --run-id=test --torch-device=cuda --time-scale=2 --torch-device=cuda --force
